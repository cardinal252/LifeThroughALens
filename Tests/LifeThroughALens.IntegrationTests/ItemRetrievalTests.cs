﻿using System;
using Autofac;
using Cardinal.Glass.Extensions.Mapping;
using Cardinal.Glass.Extensions.Mapping.Sitecore;
using Cardinal.IoC;
using Cardinal.Ioc.Autofac;
using Glass.Mapper;
using Glass.Mapper.Sc.Configuration.Fluent;
using LifeThroughALens.Domain;
using LifeThroughALens.IntegrationTests.Common;
using LifeThroughALens.Maps;
using NUnit.Framework;

namespace LifeThroughALens.IntegrationTests
{
    [TestFixture]
    public class ItemRetrievalTests : GlassTestBase
    {

        [TestFixtureSetUp]
        public void Setup()
        {           
            RegisterMapsWithContainer(ContainerManager);

            ConfigurationMap configurationMap = new ConfigurationMap(DependencyResolver);

            SitecoreFluentConfigurationLoader configloader = configurationMap.GetConfigurationLoader<SitecoreFluentConfigurationLoader>();

            SetupGlassService(configloader);
        }

        [Test]
        public void CanGetHomepageSuccessfully()
        {
            GetHomeItem();
        }

        protected override IContainerAdapter GetContainerAdapter()
        {
            ContainerBuilder builder = new ContainerBuilder();

            IContainer container = builder.Build();
            return new AutofacContainerAdapter(Guid.NewGuid().ToString(), container);
        }
    }
}
