using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Galchenko.TestTask.ApplicationLayer.Common;
using Galchenko.TestTask.ApplicationLayer.Common.Models;
using Galchenko.TestTask.Domain.Contracts;
using Microsoft.Extensions.Logging;
using p1eXu5.Wpf.MvvmBaseLibrary;
using p1eXu5.Wpf.MvvmLibrary;
using ViewModelBase = p1eXu5.Wpf.MvvmLibrary.ViewModelBase;

namespace Galchenko.TestTask.ViewModels.Contracts
{
    public abstract class CrudViewModel< TViewModel, TNewDto, TUpdateDto, TEntity, TKey > : ViewModelBase, ICrudViewModel, IErrorHandler
        where TViewModel : ViewModelBase, IIdViewModel< TKey >
        where TEntity : class, IEntityId< TKey >, new()
        where TUpdateDto : class, IEntityIdDto< TKey >
        where TNewDto : notnull, IEntityDto
        where TKey : notnull
    {
        #region fields
        private readonly ObservableCollection< TViewModel > _vmCollection;

        private TViewModel? _selectedVm;

        #endregion


        #region ctor
        protected CrudViewModel( IRepository repository,
                              DialogRepository dialogRepository,
                              ILogger logger,
                              
                              IMapper mapper )
        {
            Repository = repository;
            DialogRepository = dialogRepository;
            Mapper = mapper;
            Logger = logger;

            

            _vmCollection = new ObservableCollection<TViewModel>();
            VmCollection = new ReadOnlyObservableCollection<TViewModel>( _vmCollection );
        }

        #endregion
        
        
        #region properties
        public ReadOnlyObservableCollection<TViewModel> VmCollection { get; }

        public TViewModel? SelectedVm
        {
            get => _selectedVm; 
            set {
                _selectedVm = value;
                OnPropertyChanged();
            }
        }


        protected IRepository Repository { get; }

        protected DialogRepository DialogRepository { get; }

        

        protected IMapper Mapper { get; }
        protected ILogger Logger { get; }


        protected virtual Func< IQueryable< TEntity >, IQueryable< TEntity >>? LoadInclude { get; } = null!;

        #endregion


        #region LoadDataCommand
        public IAsyncCommand LoadDataCommand => new MvvmAsyncCommand( LoadDataAsync, errorHandler: this );

        protected virtual Task LoadDataAsync( object o )
        {
            return FillVmCollectionAsync< TUpdateDto, TViewModel >();
        }

        protected async Task FillVmCollectionAsync< TDto, TVm >()
            where TDto : notnull, IEntityDto
            where TVm : TViewModel
        {
            var dtos = await Repository.GetAllAsync< TEntity, TDto >( LoadInclude );

            _vmCollection.Clear();
            foreach (var dto in dtos) {
                if ( Activator.CreateInstance( typeof( TVm ), dto ) is TVm vm ) {
                    _vmCollection.Add( vm );
                }
                else {
                    Logger.LogError( "Cannot create entity view model.", dto );
                }
            }
        }


        #endregion


        #region CreatePatientCommand
        public virtual IAsyncCommand CreateCommand => new MvvmAsyncCommand( CreateAsync< TUpdateDto >, errorHandler: this );


        protected abstract (bool, TNewDto) CreateNewDto();

        protected async Task CreateAsync<TDto>( object o )
            where TDto : class, IEntityDto
        {
            var (isCreated, dto) = CreateNewDto();

            if ( isCreated ) {
                TKey id = await Repository.CreateAsync< TEntity, TNewDto, TKey>( dto );
                if (id is { }) {
                    await GetAndAddVmAsync<TDto, TViewModel>( id );
                }
                else {
                    ShowError( "An error occurred while creating patient." );
                }
            }
        }

        protected virtual async Task GetAndAddVmAsync< TDto, TVm >( TKey id )
            where TDto : class, IEntityDto
            where TVm : TViewModel
        {
            var (result, dto) = await Repository.GetByIdAsync<TDto, TEntity, TKey>( id );
            if (result.Succeeded) {
                AddVm<TDto, TVm>( dto! );
            }
            else {
                Logger.LogError( result.ToString() );
            }
        }

        private void AddVm< TDto, TVm >( TDto dto )
            where TDto : notnull, IEntityDto
            where TVm : TViewModel
        {
            if ( Activator.CreateInstance( typeof( TVm ), dto ) is TVm vm ) {
                _vmCollection.Add( vm );
                SelectedVm = vm;
            }
            else {
                Logger.LogError( "Cannot create entity view model.", dto );
            }
        }

        #endregion


        #region UpdateCardCommand
        public virtual IAsyncCommand UpdateCommand  => new MvvmAsyncCommand( UpdateAsync< TUpdateDto >, errorHandler: this );

        protected async Task UpdateAsync<TDto>( object o )
            where TDto : class, IEntityIdDto< TKey >
        {
            var selectedVm = SelectedVm;
            if (selectedVm is null) return;

            var (result, dto) = await Repository.GetByIdAsync<TDto, TEntity, TKey>( selectedVm.Id );
            if (result.Succeeded) {

                var (isUpdated, updateDto) = UpdateDto( dto! );

                if ( isUpdated ) {
                    result = await Repository.UpdateAsync< TUpdateDto, TEntity, TKey>( updateDto! );

                    if (result.Succeeded) {
                        if ( _vmCollection.Remove( selectedVm ) ) {
                            await GetAndAddVmAsync< TDto, TViewModel >( updateDto!.Id );
                        }
                    }
                }
            }
        }


        protected abstract (bool, TUpdateDto) UpdateDto< TDto >( TDto dto );

        #endregion


        #region DeletePatient

        public IAsyncCommand DeleteCommand => new MvvmAsyncCommand( DeleteAsync, errorHandler: this );

        private async Task DeleteAsync( object o )
        {
            var selectedVm = SelectedVm;
            if (selectedVm is null) return;

            var result = await Repository.DeleteAsync<TEntity, TKey>( selectedVm.Id );

            if (result.Succeeded) {
                _vmCollection.Remove( selectedVm );
            }
            else {
                ShowError( $"An error occurred while updating patient data. \n { result.Errors }" );
            }
        }

        #endregion


        void IErrorHandler.HandleError( Exception ex )
        {
            Logger.Log( LogLevel.Error, ex.Message );
            // ShowError( ex.Message + "\n" + ex.InnerException?.Message );
            ShowError( "An error has occurred and the application will now close." );
        }

        protected void ShowError( string message )
        {
            var vm = new ErrorViewModel( message );
            var dialog = DialogRepository.GetView( vm );
            dialog?.ShowDialog();
        }
    }
}
