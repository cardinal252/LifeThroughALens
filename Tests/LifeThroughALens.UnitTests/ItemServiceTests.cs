using System;
using System.Collections.Generic;
using Glass.Mapper.Sc;
using LifeThroughALens.Domain;
using NSubstitute;
using NUnit.Framework;

namespace LifeThroughALens.UnitTests
{
    [TestFixture]
    public class ItemServiceTests
    {
        [Test]
        public void CanGetJobsSuccessfully()
        {
            // Assign
            ISitecoreService service = Substitute.For<ISitecoreService>();
            JobHub jobHub = new JobHub();
            IEnumerable<IJob> jobs = Substitute.For<IEnumerable<IJob>>();

            jobHub.Jobs = jobs;
            service.GetItem<JobHub>(SitecoreIds.JobHubId.ToGuid()).Returns(jobHub);
            ItemService itemService = new ItemService(service);

            // Act
            var result = itemService.GetJobs();

            // Assert
            service.Received(1).GetItem<JobHub>(SitecoreIds.JobHubId.ToGuid());
            Assert.AreEqual(jobs, result);
        }

        [Test]
        public void CanGetJobsServiceException()
        {
            // Assign
            ISitecoreService service = Substitute.For<ISitecoreService>();

            service.GetItem<JobHub>(SitecoreIds.JobHubId.ToGuid()).Throws<Exception>();
            ItemService itemService = new ItemService(service);

            // Act
            var result = itemService.GetJobs();

            // Assert
            service.Received(1).GetItem<JobHub>(SitecoreIds.JobHubId.ToGuid());
            Assert.IsNull(result);
        }

        [Test]
        public void CanGetJobsServiceNullReturn()
        {
            // Assign
            ISitecoreService service = Substitute.For<ISitecoreService>();

            service.GetItem<JobHub>(SitecoreIds.JobHubId.ToGuid()).Returns(null as JobHub);
            ItemService itemService = new ItemService(service);

            // Act
            var result = itemService.GetJobs();

            // Assert
            service.Received(1).GetItem<JobHub>(SitecoreIds.JobHubId.ToGuid());
            Assert.IsNull(result);
        }
    }
}
