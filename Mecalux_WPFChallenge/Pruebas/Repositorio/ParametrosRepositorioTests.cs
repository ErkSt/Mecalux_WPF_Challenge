using Entidades.Enums;
using Repositorio.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pruebas.Repositorio
{
    [TestFixture]
    public sealed class ParametrosRepositoryEnMemoriaTests
    {
        private ParametrosRepositoryEnMemoria _repositorio;

        [SetUp]
        public void SetUp() {
            _repositorio = new ParametrosRepositoryEnMemoria();
        }

        [Test]
        public async Task GetAllOpcionesOrdenamiento_DevuelveTresOrdenamientos()
        {
            var result = (await _repositorio.GetAllOpcionesOrdenamiento()).ToList();

            Assert.That(result, Has.Count.EqualTo(3));
        }

        [Test]
        public async Task GetAllOpcionesOrdenamiento_TraeLoEsperado()
        {
            var result = (await _repositorio.GetAllOpcionesOrdenamiento()).ToList();

            Assert.Multiple(() =>
            {
                Assert.That(result.Select(x => x.Tipo), Is.EquivalentTo(new[]
                {
                TipoOrdenamiento.AlfabeticoAscentente,
                TipoOrdenamiento.AlfabeticoDescendente,
                TipoOrdenamiento.Longitud
            }));
                Assert.That(result[0].Descripcion, Is.EqualTo("Alfabetico Ascendente"));
                Assert.That(result[1].Descripcion, Is.EqualTo("Alfabetico Descendente"));
                Assert.That(result[2].Descripcion, Is.EqualTo("Por Longitud"));
            });
        }
    }
}
