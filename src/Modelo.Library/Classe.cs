using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.Library
{
    public class Classe
    {
        private Classe()
        {
        }

        public static T CloneClass<T>(object source) where T : new()
        {
            T target = new T();
            foreach (System.Reflection.PropertyInfo pi in source.GetType().GetProperties())
                if ((pi.Name != "ExtensionData") && (target.GetType().GetProperties().Where(p => p.Name == pi.Name).Count() > 0))
                    target.GetType().GetProperty(pi.Name).SetValue(target, source.GetType().GetProperty(pi.Name).GetValue(source, null), null);
            return (T)target;
        }

        public static bool HasChanges(object classeoriginal, object classecurrent)
        {
            PropertyInfo[] properties = classeoriginal.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.SetProperty);

            for (int i = 0; i < properties.Length; i++)
            {
                if (properties[i].GetCustomAttributes(typeof(NoAudit), true).Length > 0)
                    continue;
                object original = properties[i].GetValue(classeoriginal, null);
                object current = properties[i].GetValue(classecurrent, null);

                if (original == null && current == null) continue;
                if (original == null && current != null) return true;
                if (original != null && current == null) return true;
                if (!original.Equals(current)) return true;
            }
            return false;
        }

    }
}
