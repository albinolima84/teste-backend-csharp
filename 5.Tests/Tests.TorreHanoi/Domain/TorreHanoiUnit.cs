using System;
using Infrastructure.TorreHanoi.Log;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Tests.TorreHanoi.Domain
{
    [TestClass]
    public class TorreHanoiUnit
    {
        private const string CategoriaTeste = "Domain/TorreHanoi";

        private Mock<ILogger> _mockLogger;

        [TestInitialize]
        public void SetUp()
        {
            _mockLogger = new Mock<ILogger>();
            _mockLogger.Setup(s => s.Logar(It.IsAny<string>(), It.IsAny<TipoLog>()));
        }

        [TestMethod]
        [TestCategory(CategoriaTeste)]
        public void Construtor_Deve_Retornar_Sucesso()
        {
            var torre = new global::Domain.TorreHanoi.TorreHanoi(3, _mockLogger.Object);
            
            Assert.IsNotNull(torre);
            Assert.IsNotNull(torre.Id);
            Assert.AreNotEqual(torre.Id, Guid.Empty);
            
            Assert.IsNotNull(torre.Discos);
            Assert.IsTrue(torre.Discos.Count > 0);
            
            Assert.IsNotNull(torre.Origem);
            Assert.IsNotNull(torre.Intermediario);
            Assert.IsNotNull(torre.Destino);
            Assert.IsNotNull(torre.PassoAPasso);

            Assert.IsFalse(torre.Intermediario.Discos.Count > 0);
            Assert.IsFalse(torre.Destino.Discos.Count > 0);
            Assert.IsFalse(torre.PassoAPasso.Count > 0);

            Assert.AreEqual(torre.Origem.Tipo, global::Domain.TorreHanoi.TipoPino.Origem);
            Assert.AreEqual(torre.Intermediario.Tipo, global::Domain.TorreHanoi.TipoPino.Intermediario);
            Assert.AreEqual(torre.Destino.Tipo, global::Domain.TorreHanoi.TipoPino.Destino);
            
            Assert.IsTrue(torre.DataCriacao >= DateTime.Today);
            Assert.IsNull(torre.DataFinalizacao);
        }

        [TestMethod]
        [TestCategory(CategoriaTeste)]
        public void Processar_Deverar_Retornar_Sucesso()
        {
            var totalDiscos = 3;
            var passosEsperados = Math.Pow(2, totalDiscos) - 1;
            var torre = new global::Domain.TorreHanoi.TorreHanoi(totalDiscos, _mockLogger.Object);
            
            torre.Processar();
            
            Assert.AreEqual(torre.Status, global::Domain.TorreHanoi.TipoStatus.FinalizadoSucesso);
            Assert.IsTrue(torre.DataFinalizacao > torre.DataCriacao);
            Assert.IsFalse(torre.Origem.Discos.Count > 0);
            Assert.IsFalse(torre.Intermediario.Discos.Count > 0);
            Assert.AreEqual(torre.Destino.Discos.Count, totalDiscos);
            Assert.IsNotNull(torre.PassoAPasso);
            Assert.AreEqual(torre.PassoAPasso.Count, passosEsperados);
        }
    }
}
