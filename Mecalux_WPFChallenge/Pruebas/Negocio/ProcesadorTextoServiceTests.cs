using Entidades.Clases;
using Entidades.Enums;
using Entidades.Interfaces;
using Moq;
using Negocio.DTOs.Requests;
using Negocio.Services;
using Repositorio.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pruebas.Negocio
{

    [TestFixture]
    public class EquipoServiceTests
    {
        private Mock<IParametrosRepository> _mockRepositorio;
        private ProcesadorTextoService _service;


        [SetUp]
        public void Setup()
        {
            _mockRepositorio = new Mock<IParametrosRepository>();
            _service = new ProcesadorTextoService(_mockRepositorio.Object);
        }

        [Test]
        public async Task GetOpcionesOrdenamiento_LlamaARepositorio()
        {
            var esperado = new List<OpcionTipoOrdenamiento>
            {
                new() { Tipo = TipoOrdenamiento.Longitud, Descripcion = "Por Longitud" }
            };

            _mockRepositorio.Setup(p => p.GetAllOpcionesOrdenamiento()).ReturnsAsync(esperado);

            var result = (await _service.GetOpcionesOrdenamiento()).ToList();

            Assert.Multiple(() =>
            {
                Assert.That(result, Has.Count.EqualTo(1));
                Assert.That(result[0].Tipo, Is.EqualTo(TipoOrdenamiento.Longitud));
                Assert.That(result[0].Descripcion, Is.EqualTo("Por Longitud"));
            });

            _mockRepositorio.Verify(p => p.GetAllOpcionesOrdenamiento(), Times.Once);
        }

        [Test]
        public async Task AnalizarTextoVacioONulo_RetornaCeros()
        {
            var empty = await _service.AnalizarTexto(new AnalizarTextoRequest { Texto = string.Empty }, CancellationToken.None);
            Assert.Multiple(() =>
            {
                Assert.That(empty.CantidadPalabras, Is.Zero);
                Assert.That(empty.CantidadGuiones, Is.Zero);
                Assert.That(empty.CantidadEspaciosEnBlanco, Is.Zero);
            });

            var nullText = await _service.AnalizarTexto(new AnalizarTextoRequest { Texto = null! }, CancellationToken.None);
            Assert.That(nullText.CantidadPalabras, Is.Zero);
        }

        [Test]
        public async Task AnalizarTexto_CuentaEspaciosYGuionesCorrectamente()
        {
            var response = await _service.AnalizarTexto(new AnalizarTextoRequest { Texto = "DosGuiones-_ UnEspacio" }, CancellationToken.None);

            Assert.Multiple(() =>
            {
                Assert.That(response.CantidadGuiones, Is.EqualTo(2));
                Assert.That(response.CantidadEspaciosEnBlanco, Is.EqualTo(1));
            });
        }

        [Test]
        public async Task AnalizarTexto_CuentaPalabrasCorrectamente()
        {
            var r1 = await _service.AnalizarTexto(new AnalizarTextoRequest { Texto = " Una  Dos Tres   Cuatro      Cinco" }, CancellationToken.None);
            var r2 = await _service.AnalizarTexto(new AnalizarTextoRequest { Texto = "Una Dos Tres" }, CancellationToken.None);
            var r3 = await _service.AnalizarTexto(new AnalizarTextoRequest { Texto = "Una_Una Dos Tres" }, CancellationToken.None);
            var r4 = await _service.AnalizarTexto(new AnalizarTextoRequest { Texto = " Una  Dos Tres   Cuatro Cinco    " }, CancellationToken.None);

            Assert.Multiple(() =>
            {
                Assert.That(r1.CantidadPalabras, Is.EqualTo(5));
                Assert.That(r2.CantidadPalabras, Is.EqualTo(3));
                Assert.That(r3.CantidadPalabras, Is.EqualTo(3));
                Assert.That(r4.CantidadPalabras, Is.EqualTo(5));
            });
        }

        [Test]
        public async Task OrdenarTexto_NuloOVacio_RetornaStringVacio()
        {
            var r1 = await _service.OrdenarTexto(
                new OrdenarTextoRequest { Texto = null!, TipoOrdenamiento = TipoOrdenamiento.AlfabeticoAscentente },
                CancellationToken.None);
            var r2 = await _service.OrdenarTexto(
                new OrdenarTextoRequest { Texto = "   ", TipoOrdenamiento = TipoOrdenamiento.AlfabeticoAscentente },
                CancellationToken.None);

            Assert.Multiple(() =>
            {
                Assert.That(r1.TextoOrdenado, Is.EqualTo(string.Empty));
                Assert.That(r2.TextoOrdenado, Is.EqualTo(string.Empty));
            });
        }

        [Test]
        public async Task OrdenarTexto_AlfabeticoAscendente()
        {
            var response = await _service.OrdenarTexto(
                new OrdenarTextoRequest
                {
                    Texto = "abuela casa banana abuela",
                    TipoOrdenamiento = TipoOrdenamiento.AlfabeticoAscentente
                },
                CancellationToken.None);

            Assert.That(response.TextoOrdenado, Is.EqualTo("abuela abuela banana casa"));
        }

        [Test]
        public async Task OrdenarTexto_AlfabeticoDescendente()
        {
            var response = await _service.OrdenarTexto(
                new OrdenarTextoRequest
                {
                    Texto = "Wisin Yandel Zebra Xilofono",
                    TipoOrdenamiento = TipoOrdenamiento.AlfabeticoDescendente
                },
                CancellationToken.None);

            Assert.That(response.TextoOrdenado, Is.EqualTo("Zebra Yandel Xilofono Wisin"));
        }

        [Test]
        public async Task OrdenarTexto_Longitud()
        {
            var response = await _service.OrdenarTexto(
                new OrdenarTextoRequest
                {
                    Texto = "dosdos uno trestrestres",
                    TipoOrdenamiento = TipoOrdenamiento.Longitud
                },
                CancellationToken.None);

            Assert.That(response.TextoOrdenado, Is.EqualTo("uno dosdos trestrestres"));
        }
    }
}
