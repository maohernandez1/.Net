namespace SuperPowerfuls.UnitTest
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using SuperPowerfuls.Core.Contracts;
    using SuperPowerfuls.Core.Services;
    using System.Collections.Generic;
    using SuperPowerfuls.Core.Models;
    using SuperPowerfuls.Core.OutputModels;
    [TestClass]
    public class SuperPowerfulsServiceTest
    {
        private Mock<ISuperPowerRepository> _mockSuperPowerfulRepository;
        private Mock<IVillianRepository> _mockVillianRepository;
        private Mock<IHeroRepository> _mockHeroRepository;
        private ISuperPowersService _service;
        Mock<IUnitOfWork> _mockUnitWork;
        List<Hero> heroes;
        List<Villian> villians;
        List<SuperPowerful> superPowerfuls;
        ClassifyOutput superPowerfulsOutput;

        [TestInitialize]
        public void Initialize()
        {
            _mockSuperPowerfulRepository = new Mock<ISuperPowerRepository>();
            _mockVillianRepository = new Mock<IVillianRepository>();
            _mockHeroRepository = new Mock<IHeroRepository>();
            _mockUnitWork = new Mock<IUnitOfWork>();
            SuperPowersfulService superPowerfulService = new SuperPowersfulService(_mockSuperPowerfulRepository.Object, _mockVillianRepository.Object, _mockHeroRepository.Object);
            superPowerfulService._flagVillian = "D";
            _service = superPowerfulService;
            heroes = new List<Hero>() { new Hero("Batman"), new Hero("Superman"), new Hero("Mujer Maravilla"), new Hero("Spiderman") };
            villians = new List<Villian>() { new Villian("Derek Devil"), new Villian("Darkseid"), new Villian("Detective|Marciano") };
            superPowerfuls = new List<SuperPowerful>() { new SuperPowerful("Batman"), new SuperPowerful("Superman"), new SuperPowerful("Mujer Maravilla"), new SuperPowerful("Derek Devil"), new SuperPowerful("Darkseid"), new SuperPowerful("Detective|Marciano"), new Hero("Spiderman") };
        }
        [TestMethod]
        public void Superpowerfuls_Classifieds_By_Heroes()
        {
            //Arrange
            _mockSuperPowerfulRepository.Setup(x => x.GetAll()).Returns(superPowerfuls);
            //Act
            heroes = _service.ClassifyByHeroes(superPowerfuls);
            //Assert
            Assert.IsNotNull(heroes);
            Assert.AreEqual(4, heroes.Count);
        }
        [TestMethod]
        public void Superpowerfuls_Classifieds_By_Villians()
        {
            //Arrange
            _mockSuperPowerfulRepository.Setup(x => x.GetAll()).Returns(superPowerfuls);
            //Act
            villians = _service.ClassifyByVillians(superPowerfuls);
            //Assert
            Assert.IsNotNull(villians);
            Assert.AreEqual(3, villians.Count);
        }
        [TestMethod]
        public void Superpowerfuls_Classifieds_By_Villians_And_Heroes_And_All()
        {
            //Arrange
            _mockSuperPowerfulRepository.Setup(x => x.GetAll()).Returns(superPowerfuls);
            //Act
            superPowerfulsOutput = _service.GetAllSuperPowerfulsAndClassify();
            //Assert
            Assert.IsNotNull(superPowerfulsOutput);
            Assert.IsNotNull(superPowerfulsOutput.Heroes);
            Assert.IsNotNull(superPowerfulsOutput.SuperPowerFul);
            Assert.IsNotNull(superPowerfulsOutput.Villians);
            Assert.AreEqual(4, superPowerfulsOutput.Heroes.Count);
            Assert.AreEqual(3, superPowerfulsOutput.Villians.Count);
            Assert.AreEqual(7, superPowerfulsOutput.SuperPowerFul.Count);
        }

        [TestMethod]
        public void Add_All_The_Heroes()
        {            
            //Arrange
            _mockHeroRepository.Setup(x => x.UnitOfWork.Commit(heroes)).Returns(4);
            //Act
            int result = _service.AddHeroes(heroes);
            //Assert
            Assert.AreEqual(4, result);
        }

        [TestMethod]
        public void Add_All_The_VIllians()
        {
            //Arrange
            _mockVillianRepository.Setup(x => x.UnitOfWork.Commit(villians)).Returns(3);
            //Act
            int result = _service.AddVillian(villians);
            //Assert
            Assert.AreEqual(3, result);
        }
    }
}
