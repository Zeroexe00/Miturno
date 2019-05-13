[assembly: Xamarin.Forms.Dependency(typeof(MasterDetail.iOS.Implementacion.PathService))]
namespace MasterDetail.iOS.Implementacion
{
    using Interface;
    using System;
    using System.IO;

    public class PathService : IPathService
    {
        public string GetDataBasePath()
        {
            string docFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string libFolder = Path.Combine(docFolder, "..", "Library", "Databases");

            if (!Directory.Exists(libFolder))
            {
                Directory.CreateDirectory(libFolder);
            }
            return Path.Combine(libFolder, "MiTurno.db3");
        }
    }
}