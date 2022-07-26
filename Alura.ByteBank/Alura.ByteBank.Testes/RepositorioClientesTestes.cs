using Moq;
using Xunit;

using System;
using System.Collections.Generic;

using Microsoft.Extensions.DependencyInjection;

using Alura.ByteBank.Dados.Repositorio;
using Alura.ByteBank.Dominio.Entidades;
using Alura.ByteBank.Infraestrutura.Testes.Servicos;
using Alura.ByteBank.Dominio.Interfaces.Repositorios;

namespace Alura.ByteBank.Testes
{
    public class RepositorioClientesTestes
    {
        private readonly IClienteRepositorio _clienteRepositorio;

        public RepositorioClientesTestes()
        {
            var servico = new ServiceCollection();

            servico.AddTransient<IClienteRepositorio, ClienteRepositorio>();
            var provedor = servico.BuildServiceProvider();

            _clienteRepositorio = provedor.GetService<IClienteRepositorio>();
        }

        [Fact]
        public void TestaObterTodosClientes()
        {
            // Arrange
            var _repositorio = new ClienteRepositorio();

            // Act
            List<Cliente> lista = _repositorio.ObterTodos();

            // Assert
            Assert.NotNull(lista);

            // Assertion para um caso específico, refatorar teste conforme necessidade
            Assert.Equal(5, lista.Count);
        }

        [Fact]
        public void TestaObterClientePorId()
        {
            // Arrange
            // Act
            var cliente = _clienteRepositorio.ObterPorId(1);

            // Assert
            Assert.NotNull(cliente);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void TestaObterClientesPorVariosIds(int id)
        {
            // Arrange
            // Act
            var cliente = _clienteRepositorio.ObterPorId(id);

            // Assert
            Assert.NotNull(cliente);
        }

        [Fact]
        public void TesteInsereUmNovoClienteNaBaseDeDados()
        {
            //Arrange            
            string nome = "Alberto Roberto";
            string cpf = "088.157.930-03";
            Guid identificador = Guid.NewGuid();
            string profissao = "Administrador de Empresas";

            var cliente = new Cliente()
            {
                Nome = nome,
                CPF = cpf,
                Identificador = identificador,
                Profissao = profissao
            };

            //Act
            var retorno = _clienteRepositorio.Adicionar(cliente);

            //Assert
            Assert.True(retorno);
        }

        [Fact]
        public void TestaAtualizacaoInformacaoDeterminadoCliente()
        {
            //Arrange
            //_repositorio = new ClienteRepositorio();
            var cliente = _clienteRepositorio.ObterPorId(2);
            var nomeNovo = "João Pedro";
            cliente.Nome = nomeNovo;

            //Act
            var atualizado = _clienteRepositorio.Atualizar(2, cliente);

            //Assert
            Assert.True(atualizado);
        }

        // Testes com Mock
        [Fact]
        public void TestaObterClientesMock()
        {
            //Arange
            var bytebankRepositorioMock = new Mock<IByteBankRepositorio>();
            var mock = bytebankRepositorioMock.Object;

            //Act
            var lista = mock.BuscarClientes();

            //Assert
            bytebankRepositorioMock.Verify(b => b.BuscarClientes());
        }

    }
}