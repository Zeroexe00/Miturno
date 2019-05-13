[assembly: Xamarin.Forms.Dependency(typeof(MasterDetail.Droid.Implementacion.PathService))]
namespace MasterDetail.Droid.Implementacion
{
    using Interface;
    using System;
    using System.IO;

    public class PathService : IPathService
    {
        public string GetDataBasePath()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            return Path.Combine(path, "MiTurno.db3");
        }
    }
}