using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.AspNetCore;
using HotChocolate.Types;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SharpathonTask.Contracts;

namespace ApiEndpoint
{
	public class CustomerType : ObjectType<Customer>
	{
		protected override void Configure(IObjectTypeDescriptor<Customer> descriptor)
		{
			descriptor.Field(f => f.CustomerId).Type<NonNullType<IntType>>();
			descriptor.Field(f => f.CustomerName).Type<NonNullType<StringType>>();
			descriptor.Field(f => f.CustomerType).Type<NonNullType<EnumType<SharpathonTask.Contracts.CustomerType>>>();
		}
	}


	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

			services.AddGraphQL(
				sp => Schema.Create(
				@"
				type Customer {
				  customerId: Int!
				  customerName: String!
				  customerType: CustomerType!
				}

				enum CustomerType {
				  Individual
				  Organization
				}

				type Contract {
				  contractCode: String!
				  customerId: Int!
				  marketingCategory: SimpleEntity
				  legalCategory: LegalCategory!
				}

				enum LegalCategory {
				  Resident
				  Nonresident
				}

				type SimpleEntity {
				  code: String!
				  name: String!
				}

				type PersonalAccount {
				  personalAccountId: ID!
				  contractId: ID!
				  calculationMethod: CalculationMethod
				  currencyCode: String
				  serviceProvider: SimpleEntity!
				}

				enum CalculationMethod {
				  Prepaid
				  Postpaid
				}

				type TerminalDevice {
				  msisdn: String
				  personalAccountCode: String
				  tariffPlan: SimpleEntity
				}

				type Service {
				  code: String!
				  name: String!
				}

				enum TarificationOption {
				  Periodic,
				  Counter
				}

				type Query {
				  customerDevices(
					customerId: Int!
					page: Int! = 0
					itemsPerPage: Int! = 25
				  ): [TerminalDevice!]
  
				  accountServices(
  					accountCode: String!
					tarificationFilter: TarificationOption,
					page: Int! = 0,
					itemsPerPage: Int! = 25
				  ): [Service!]
				}

				type Mutation {
				  addService(
					serviceCode: String!
					devicesMsisdns: [String!]
				  ): Boolean
				}

				schema {
				  query: Query
				  mutation: Mutation
				}",
				c => {
					c.BindType<Customer>();
					c.BindType<Contract>();
					c.BindType<SimpleEntity>();
					c.BindType<PersonalAccount>();
					c.BindType<TerminalDevice>();
					c.BindType<Service>();
					c.BindType<Query>();
					c.BindType<Mutation>();
				}));
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseGraphQL("/graphql");
		}
	}
}
