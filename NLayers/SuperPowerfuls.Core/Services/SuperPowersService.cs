namespace SuperPowerfuls.Core.Services
{
    using SuperPowerfuls.Core.Contracts;
    using SuperPowerfuls.Core.Models;
    using SuperPowerfuls.Core.OutputModels;
    using System.Collections.Generic;
    using System.Linq;
    using SuperPowerfuls.Core.Factories;
    using System;
    using SuperPowerfuls.Commons.Log;
    using SuperPowerfuls.Commons.Config;
    /// <summary>
    /// Fecha: 20-11-2015
    /// Autor: Wilson Mauricio Hernandez
    /// Descripción: Tiene la responsabilidad de gestionar las operaciones de clasificar los superheroes, retornar el listado de los mismos y guarda en archivo .DAT aparte
    ///              Villanos y Heroes.
    /// </summary>
    public class SuperPowersfulService : ISuperPowersService
    {
        #region propiedades
        private readonly ISuperPowerRepository _superPowerRepository;
        private readonly IVillianRepository _villianRepository;
        private readonly IHeroRepository _heroReposiory;
        public string _flagVillian { get; set; }
        SuperPowerfulFactory Factoria = new SuperPowerfulFactory();//no implementada en esta versión
        #endregion

        #region constructures
        public SuperPowersfulService(ISuperPowerRepository superPowerRepository, IVillianRepository villianRepository, IHeroRepository heroReposiory)
        {
            _superPowerRepository = superPowerRepository;
            _villianRepository = villianRepository;
            _heroReposiory = heroReposiory;
            _flagVillian = ConfigApp.GetFlagForVillian();
        }
        #endregion

        #region metodos de clasificación
        /// <summary>
        /// Clasifica y toma solo aquellos objetos que pertenecen a la clase Hero utilizando el patron especificación.
        /// </summary>
        /// <param name="superPowerful">Lista de objetos SuperPowerful del archivo .DAT</param>
        /// <returns>List<Hero></returns>
        public List<Hero> ClassifyByHeroes(List<SuperPowerful> superPowerful)
        {
            //REFACTORIZAR JUANTO A ClassifyByVillians
            List<Hero> heroes = new List<Hero>();
            try
            {
                //ISpecification<SuperPowerful> villiansSpecification = new ExpressionSpecification<SuperPowerful>(o => !o.Nombre[0].ToString().Equals(_flagVillian));
                var superPowerfulCollection = superPowerful.Where(o => !o.Name.Contains(_flagVillian));//_superPowerRepository.AllMatching(heroesSpecification);
                
                foreach (var item in superPowerfulCollection)
                {
                    heroes.Add(new Hero(item.Name));
                }
            }
            catch(ArgumentNullException argException)
            {
                ErrorManager.RiseError(argException.Message);
            }
            catch (ArgumentException argException)
            {
                ErrorManager.RiseError(argException.Message);
            }
            catch(Exception exception)
            {
                ErrorManager.RiseError(exception.Message);
            }
            return heroes; 
        }       

        /// <summary>
        /// Clasifica y toma solo aquellos objetos que pertenecen a la clase Villian utilizando el patron especificación..
        /// </summary>
        /// <param name="superPowerful">Lista de objetos SuperPowerful del archivo .DAT</param>
        /// <returns>List<Villian></returns>
        public List<Villian> ClassifyByVillians(List<SuperPowerful> superPowerful)
        {
            List<Villian> villians = new List<Villian>();
            try
            {
                //ISpecification<SuperPowerful> villiansSpecification = new ExpressionSpecification<SuperPowerful>(o => o.Nombre[0].ToString().Equals(_flagVillian));
                var superPowerfulCollection = superPowerful.Where(o => o.Name.Contains(_flagVillian)); //_superPowerRepository.AllMatching(villiansSpecification);

                foreach (var item in superPowerfulCollection)
                {
                    villians.Add(new Villian(item.Name));
                }
            }
            catch (ArgumentNullException argException)
            {
                ErrorManager.RiseError(argException.Message);
            }
            catch (ArgumentException argException)
            {
                ErrorManager.RiseError(argException.Message);
            }
            catch (Exception exception)
            {
                ErrorManager.RiseError(exception.Message);
            }
            return villians;            
        }
        #endregion

        #region metodos acceso a datos
        /// <summary>
        /// Obtiene y retorna todos los objetos del tipo Hero, Villian y SuperPowerFul.
        /// </summary>
        /// <returns>ClassifyOutput</returns>
        public ClassifyOutput GetAllSuperPowerfulsAndClassify()
        {
            ClassifyOutput SuperPowerfulClassified = new ClassifyOutput();
            try
            {
                var superPowerful = _superPowerRepository.GetAll().ToList();
                var heroes = ClassifyByHeroes(superPowerful);
                var villians = ClassifyByVillians(superPowerful);
                SuperPowerfulClassified.Heroes = heroes;
                SuperPowerfulClassified.SuperPowerFul = superPowerful;
                SuperPowerfulClassified.Villians = villians;
            }
            catch(Exception exception)
            {
                ErrorManager.RiseError(exception.Message);
            }
            return SuperPowerfulClassified;
        }
        #endregion

        #region metodos para agregar entidades

        /// <summary>
        /// Adds the villians and heroes.
        /// </summary>
        /// <param name="villians">The villians.</param>
        /// <param name="heroes">The heroes.</param>
        /// <returns></returns>
        public string AddVilliansAndHeroes(List<Villian> villians, List<Hero> heroes)
        {
            int villiansSaved, villiansToSave, heroesSaved, heroesToSave;
            string message = string.Empty;

            villiansToSave = villians.Count;
            heroesToSave = heroes.Count;

            villiansSaved =  villiansToSave > 0 ? AddVillian(villians) : 0;
            heroesSaved = heroesToSave > 0 ? AddHeroes(heroes) : 0;

            //REFACTORIZAR

            if((villiansSaved == villiansToSave) && (heroesSaved == heroesToSave))
            {
                message = "Villanos y Heroes clasificados con exito";
            }
            else if((villiansSaved == villiansToSave) && (heroesSaved != heroesToSave))
            {
                message = "Heroes no clasificados por completo";
            }
            else if ((villiansSaved != villiansToSave) && (heroesSaved == heroesToSave))
            {
                message = "Villanos no clasificados por completo";
            }
            else if ((villiansSaved != villiansToSave) && (heroesSaved != heroesToSave))
            {
                message = "Villanos y Heroes no clasificados correctamente";
            }
            return message;

        }
        #endregion


        /// <summary>
        /// Adds the villian.
        /// </summary>
        /// <param name="villians">The villians.</param>
        /// <returns></returns>
        public int AddVillian(List<Villian> villians)
        {
            int result = -1;
            try
            {
                result = _villianRepository.UnitOfWork.Commit(villians);
            }
            catch (Exception exception)
            {
                ErrorManager.RiseError(exception.Message);
            }
            return result;
        }

        /// <summary>
        /// Adds the heroes.
        /// </summary>
        /// <param name="heroes">The heroes.</param>
        /// <returns></returns>
        public int AddHeroes(List<Hero> heroes)
        {
            int result = -1;
            try
            {
                result = _heroReposiory.UnitOfWork.Commit(heroes);
            }
            catch (Exception exception)
            {
                ErrorManager.RiseError(exception.Message);
            }
            return result;
        }
    }
}
