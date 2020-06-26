using System.Linq;

namespace Galchenko.TestTask.ApplicationLayer.Common.Extensions
{
    public class PropertyCopier<TParent, TChild> where TParent : class
                                                 where TChild : class
    {
        public static void Copy(TParent parent, TChild child, params string[] excludeProps)
        {
            var parentProperties = parent.GetType().GetProperties();
            var childProperties = child.GetType().GetProperties();

            for (int i = 0; i < parentProperties.Count() - 1; i++)
            {
                if (excludeProps.Any(c=>c.Equals(parentProperties[i].Name)))
                    continue;

                var value = childProperties.FirstOrDefault(c => c.Name.Equals(parentProperties[i].Name));
                parentProperties[i].SetValue(parent, value?.GetValue(child));
            }
        }
    }
}
