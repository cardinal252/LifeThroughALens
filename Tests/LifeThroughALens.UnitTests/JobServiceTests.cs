using System;
using System.Collections.Generic;
using System.Linq;
using Glass.Mapper.Sc;
using LifeThroughALens.Domain;
using NSubstitute;
using NUnit.Framework;

namespace LifeThroughALens.UnitTests
{
    /// <summary>
    /// Job hub page unit tests
    /// </summary>
    [TestFixture]
    public class JobServiceTests
    {
        /// <summary>
        /// Test to ensure that the job hub page gets all the current active jobs
        /// </summary>
        [Test]
        public void CanGetActiveJobsSuccessfully()
        {
            // Assign
            ISitecoreService glassService = Substitute.For<ISitecoreService>();
            JobHub jobHub = new JobHub();
            var job1 = Substitute.For<IJob>();
            job1.DeadlineDateTime = DateTime.Today.AddDays(-1);
            var job2 = Substitute.For<IJob>();
            job2.DeadlineDateTime = DateTime.Today.AddDays(1);
            IEnumerable<IJob> jobs = new[] {job1, job2};
            ILog log = Substitute.For<ILog>();

            jobHub.Jobs = jobs;
            glassService.GetItem<JobHub>(SitecoreIds.JobHubId.ToGuid()).Returns(jobHub);
            JobsService jobsService = new JobsService(glassService, log);

            // Act
            var result = jobsService.GetLiveJobs().ToArray();

            // Assert
            glassService.Received(1).GetItem<JobHub>(SitecoreIds.JobHubId.ToGuid());
            log.Received(0).LogException(Arg.Any<string>(), Arg.Any<Exception>());
            Assert.AreEqual(1, result.Length);
            Assert.AreEqual(job2, result.FirstOrDefault());
        }

        /// <summary>
        /// Test to ensure that the job hub page behaves when there are no active jobs
        /// </summary>
        [Test]
        public void CanGetActiveJobsNoActiveJobs()
        {
            // Assign
            ISitecoreService glassService = Substitute.For<ISitecoreService>();
            JobHub jobHub = new JobHub();
            ILog log = Substitute.For<ILog>();

            glassService.GetItem<JobHub>(SitecoreIds.JobHubId.ToGuid()).Returns(jobHub);
            JobsService jobsService = new JobsService(glassService, log);

            // Act
            var result = jobsService.GetLiveJobs();

            // Assert
            glassService.Received(1).GetItem<JobHub>(SitecoreIds.JobHubId.ToGuid());
            log.Received(0).LogException(Arg.Any<string>(), Arg.Any<Exception>());
            Assert.IsNull(result);
        }

        /// <summary>
        /// Test to ensure that the job hub page behaves gracefully an exception is thrown from the service class
        /// </summary>
        [Test]
        public void CanGetActiveJobsServiceException()
        {
            // Assign
            ISitecoreService glassService = Substitute.For<ISitecoreService>();
            ILog log = Substitute.For<ILog>();

            glassService.GetItem<JobHub>(SitecoreIds.JobHubId.ToGuid()).Throws<Exception>();
            JobsService jobsService = new JobsService(glassService, log);

            // Act
            var result = jobsService.GetLiveJobs();

            // Assert
            glassService.Received(1).GetItem<JobHub>(SitecoreIds.JobHubId.ToGuid());
            log.Received(1).LogException(Arg.Any<string>(), Arg.Any<Exception>());
            Assert.IsNull(result);
        }

        [Test]
        public void CanGetActiveJobsServiceNullReturn()
        {
            // Assign
            ISitecoreService glassService = Substitute.For<ISitecoreService>();
            ILog log = Substitute.For<ILog>();

            glassService.GetItem<JobHub>(SitecoreIds.JobHubId.ToGuid()).Returns(null as JobHub);
            JobsService jobsService = new JobsService(glassService, log);

            // Act
            var result = jobsService.GetLiveJobs();

            // Assert
            glassService.Received(1).GetItem<JobHub>(SitecoreIds.JobHubId.ToGuid());
            log.Received(0).LogException(Arg.Any<string>(), Arg.Any<Exception>());
            Assert.IsNull(result);
        }
    }
}
