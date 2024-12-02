﻿// <auto-generated />
using System;
using CupcakeStoreAPI.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CupcakeStoreAPI.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20241128232111_AdicionarPedidoEPagamento")]
    partial class AdicionarPedidoEPagamento
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("CupcakeStoreAPI.Domain.Models.Avaliacao", b =>
                {
                    b.Property<int>("AvaliacaoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("AvaliacaoId"));

                    b.Property<string>("Comentario")
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)");

                    b.Property<int>("Nota")
                        .HasColumnType("integer");

                    b.Property<int>("ProdutoId")
                        .HasColumnType("integer");

                    b.HasKey("AvaliacaoId");

                    b.HasIndex("ProdutoId");

                    b.ToTable("Avaliacao");
                });

            modelBuilder.Entity("CupcakeStoreAPI.Domain.Models.Categoria", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Descricao")
                        .HasMaxLength(300)
                        .HasColumnType("character varying(300)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("Id");

                    b.ToTable("Categorias");
                });

            modelBuilder.Entity("CupcakeStoreAPI.Domain.Models.Endereco", b =>
                {
                    b.Property<int>("EnderecoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("EnderecoId"));

                    b.Property<string>("Bairro")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("Cep")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<string>("Cidade")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("Complemento")
                        .HasMaxLength(300)
                        .HasColumnType("character varying(300)");

                    b.Property<string>("Numero")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("character varying(15)");

                    b.Property<string>("Rua")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("character varying(150)");

                    b.HasKey("EnderecoId");

                    b.ToTable("Enderecos");
                });

            modelBuilder.Entity("CupcakeStoreAPI.Domain.Models.Pagamento", b =>
                {
                    b.Property<int>("PagamentoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("PagamentoId"));

                    b.Property<DateTime>("DataPagamento")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("FormaPagamento")
                        .HasColumnType("integer");

                    b.Property<int?>("PedidoId")
                        .HasColumnType("integer");

                    b.Property<double>("Valor")
                        .HasColumnType("numeric(12,2)");

                    b.HasKey("PagamentoId");

                    b.HasIndex("PedidoId");

                    b.ToTable("Pagamentos");
                });

            modelBuilder.Entity("CupcakeStoreAPI.Domain.Models.Pedido", b =>
                {
                    b.Property<int>("PedidoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("PedidoId"));

                    b.Property<DateTime>("DataPedido")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("EnderecoId")
                        .HasColumnType("integer");

                    b.Property<double>("ValorTotal")
                        .HasColumnType("double precision");

                    b.HasKey("PedidoId");

                    b.HasIndex("EnderecoId");

                    b.ToTable("Pedidos");
                });

            modelBuilder.Entity("CupcakeStoreAPI.Domain.Models.Produto", b =>
                {
                    b.Property<int>("ProdutoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ProdutoId"));

                    b.Property<int>("CategoriaId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("DataCadastro")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Descricao")
                        .HasMaxLength(300)
                        .HasColumnType("character varying(300)");

                    b.Property<int>("Estoque")
                        .HasColumnType("integer");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<int?>("PedidoId")
                        .HasColumnType("integer");

                    b.Property<double>("Preco")
                        .HasColumnType("decimal(10,2)");

                    b.Property<int>("QuantidadeVendido")
                        .HasColumnType("integer");

                    b.HasKey("ProdutoId");

                    b.HasIndex("CategoriaId");

                    b.HasIndex("PedidoId");

                    b.ToTable("Produtos");
                });

            modelBuilder.Entity("CupcakeStoreAPI.Domain.Models.Avaliacao", b =>
                {
                    b.HasOne("CupcakeStoreAPI.Domain.Models.Produto", "Produto")
                        .WithMany("Avaliacoes")
                        .HasForeignKey("ProdutoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Produto");
                });

            modelBuilder.Entity("CupcakeStoreAPI.Domain.Models.Pagamento", b =>
                {
                    b.HasOne("CupcakeStoreAPI.Domain.Models.Pedido", null)
                        .WithMany("Pagamentos")
                        .HasForeignKey("PedidoId");
                });

            modelBuilder.Entity("CupcakeStoreAPI.Domain.Models.Pedido", b =>
                {
                    b.HasOne("CupcakeStoreAPI.Domain.Models.Endereco", "Endereco")
                        .WithMany()
                        .HasForeignKey("EnderecoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Endereco");
                });

            modelBuilder.Entity("CupcakeStoreAPI.Domain.Models.Produto", b =>
                {
                    b.HasOne("CupcakeStoreAPI.Domain.Models.Categoria", "Categoria")
                        .WithMany("Produtos")
                        .HasForeignKey("CategoriaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CupcakeStoreAPI.Domain.Models.Pedido", null)
                        .WithMany("Produtos")
                        .HasForeignKey("PedidoId");

                    b.Navigation("Categoria");
                });

            modelBuilder.Entity("CupcakeStoreAPI.Domain.Models.Categoria", b =>
                {
                    b.Navigation("Produtos");
                });

            modelBuilder.Entity("CupcakeStoreAPI.Domain.Models.Pedido", b =>
                {
                    b.Navigation("Pagamentos");

                    b.Navigation("Produtos");
                });

            modelBuilder.Entity("CupcakeStoreAPI.Domain.Models.Produto", b =>
                {
                    b.Navigation("Avaliacoes");
                });
#pragma warning restore 612, 618
        }
    }
}