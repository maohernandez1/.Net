namespace SuperPowerfuls.Infraestructure.TXTContext
{
    using SuperPowerfuls.Core.Models;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using SuperPowerfuls.Commons.Constants;
    using SuperPowerfuls.Commons.Log;

    #region enumeraciones    
    public enum EntityModel
    {
        SuperPowerful,
        Hero,
        Villian
    }
    #endregion    

    /// <summary>
    /// Autor: Wilson Mauricio Hernandez
    /// Fecha: 21-11-2015
    /// Descripción: Tiene como responsabilidad el controlar el acceso a datos provenientes de archivos .DAT
    /// </summary>
    public class DbContext:IDisposable
    {
        #region propiedades        
        /// <summary>
        /// Gets or sets the path.
        /// </summary>
        /// <value>
        /// The path.
        /// </value>
        public string _path { get; set; }
        #endregion

        #region constructores        
        /// <summary>
        /// Initializes a new instance of the <see cref="DbContext"/> class.
        /// </summary>
        public DbContext()
        {

        }
        #endregion

        #region metodos        
        /// <summary>
        /// Get set of T (Villian, Hero, SuperPowerful) from File
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>List of T</returns>
        public virtual IEnumerable<T> Set<T>()
            where T : class
        {
            List<T> entities = new List<T>();
            _path = string.Empty;
            try
            {
                //REFACTORIZAR DRY-------------------------------------------------------------------------------------------------------
                EntityModel objectType;
                if (Enum.TryParse(typeof(T).Name, out objectType))
                {
                    switch (objectType)
                    {
                        case EntityModel.Hero:
                            _path = System.AppDomain.CurrentDomain.BaseDirectory + SuperPowerfulConstants.RouteHeroesFile;
                            break;
                        case EntityModel.Villian:
                            _path = System.AppDomain.CurrentDomain.BaseDirectory + SuperPowerfulConstants.RouteVilliansFile;
                            break;
                        case EntityModel.SuperPowerful:
                            _path = System.AppDomain.CurrentDomain.BaseDirectory + SuperPowerfulConstants.RouteSuperPowerfulsFile;
                            break;
                        default:
                            break;
                    }
                    //------------------------------------------------------------------------------------------------------------------
                    if (!File.Exists(_path))
                    {
                        File.Create(_path);
                    }

                    using (var reader = File.OpenText(_path))
                    {
                        string entityName = typeof(T).Name;
                        string name;


                        while ((name = reader.ReadLine()) != null)
                        {
                            entities.Add((T)Activator.CreateInstance(typeof(T), new object[] { name }));
                        }

                    }
                }
                else
                {
                    ErrorManager.RiseError("El tipo de entidad " + typeof(T).Name + " no existe");
                }                           
            }
            catch (Exception exception)
            {
                ErrorManager.RiseError(exception.Message);
            }
            return entities;
        }

        /// <summary>
        /// Saves the changes.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entities">The entities to save</param>
        /// <returns>1 success, 0 error</returns>
        public virtual int SaveChanges<T>(List<T> entities) where T : class
        {
            EntityModel objectType;
            int result = -1;
            if (Enum.TryParse(typeof(T).Name, out objectType))
            {
                result = SaveFile<T>(objectType, entities);
            }
            else
            {
                ErrorManager.RiseError("El tipo de entidad " + typeof(T).Name + " no existe");
            }

            return result;
        }

        /// <summary>
        /// Saves the file.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objectType">Type of the object to save.</param>
        /// <param name="entities">List of entities.</param>
        /// <returns>int numbre of entities saved</returns>
        public int SaveFile<T>(EntityModel objectType, List<T> entities) where T : class
        {
            List<string> names = new List<string>();
            int entitiesToSave = entities.Count;
            int entitiesSaved = 0;
            _path = string.Empty;
            try
            {
                switch (objectType)
                {
                    case EntityModel.Hero:
                        _path = System.AppDomain.CurrentDomain.BaseDirectory + SuperPowerfulConstants.RouteHeroesFile;
                        names = (from item in entities select item as Hero).Select(x => x.Name).ToList();
                        break;
                    case EntityModel.Villian:
                        _path = System.AppDomain.CurrentDomain.BaseDirectory + SuperPowerfulConstants.RouteVilliansFile;
                        names = (from item in entities select item as Villian).Select(x => x.Name).ToList();
                        break;
                    default:
                        break;
                }
                entitiesSaved = names.Count;
                File.WriteAllLines(_path, names.ToArray(), Encoding.UTF8);
                if (entitiesSaved != entitiesToSave)
                {
                    ErrorManager.RiseError("Cantidad de entidades guardadas es diferentes a las entidades que deberierón ser guardadas");
                }
            }
            catch(System.IO.FileNotFoundException fielNotFoundException)
            {
                ErrorManager.RiseError(fielNotFoundException.Message);
            }
            catch (System.IO.IOException ioException)
            {
                ErrorManager.RiseError(ioException.Message);
            }
            catch (Exception exception)
            {
                ErrorManager.RiseError(exception.Message);
            }

            return names.Count;
        }

        public void Dispose()
        {
            this.Dispose();
        }
        #endregion
    }
}
