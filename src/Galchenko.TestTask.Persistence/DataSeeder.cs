using System;
using System.Linq;
using Galchenko.TestTask.ApplicationLayer.Common;
using Galchenko.TestTask.Domain;
using Galchenko.TestTask.Domain.Enums;

namespace Galchenko.TestTask.Persistence
{
    public static class DataSeeder
    {
        public static void SeedData( IApplicationDbContext dbContext )
        {
            SeedPatients( dbContext );
        }

        private static void SeedPatients( IApplicationDbContext dbContext )
        {
            if ( !dbContext.Patients.Any() )
            {
                var patients = new[] {
                    new Patient {
                        FirstName = "John",
                        LastName = "Doe",
                        Gender = Gender.Male,
                        DateOfBirth = new DateTime( 1981, 11, 13 ),
                        Address = new Address {
                            City = "New York",
                            PostalCode = "NY-1345",
                            Line1 = "5th Avenue, 5"
                        },
                        Phone = "555-55-55",
                        Appointments = new[] {
                            new Appointment {
                                Date = DateTimeOffset.UtcNow,
                                Type = AppointmentType.Initial,
                                Diagnosis = "John Doe diagnosis"
                            }, 
                        }
                    }
                };

                dbContext.AddRange( patients );
                dbContext.SaveChanges();
            }
        }
    }
}
