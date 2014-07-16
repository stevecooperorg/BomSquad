using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BomSquad.Test
{
    [TestClass]
    public class BomSquadTaskTests
    {
        [TestMethod]
        public void ShouldSucceedWhenRequiredBomExists()
        {
            bool shouldHaveBom = true;
            string bomType = "UTF-8";
            var expected = true;

            Test(shouldHaveBom, bomType, expected);
        }

        [TestMethod]
        public void ShouldFailWhenRequiredBomDoesNotExists()
        {
            bool shouldHaveBom = true;
            string bomType = null;
            var expected = false;

            Test(shouldHaveBom, bomType, expected);
        }

        [TestMethod]
        public void ShouldFailOnMissingBom()
        {
            bool shouldHaveBom = true;
            string bomType = null;
            var expected = false;

            Test(shouldHaveBom, bomType, expected);
        }

        [TestMethod]
        public void ShouldSucceedOnAsciiBom()
        {
            bool shouldHaveBom = false;
            string bomType = null;
            var expected = true;

            Test(shouldHaveBom, bomType, expected);
        }

        private static void Test(bool shouldHaveBom, string bomType, bool expected)
        {
            var helper = new BomFileHelper();
            var file = helper.CreateFileWithBom(bomType);

            var task = new BomSquad.BuildTasks.BomSquadTask();
            task.ShouldHaveBom = shouldHaveBom;


            var buildEngine = new Mock<IBuildEngine>(MockBehavior.Strict);

            buildEngine.Setup(be => be.LogMessageEvent(It.IsAny<BuildMessageEventArgs>()));
            buildEngine.Setup(be => be.ProjectFileOfTaskNode).Returns("MockFile");
            buildEngine.Setup(be => be.LineNumberOfTaskNode).Returns(1);
            buildEngine.Setup(be => be.ColumnNumberOfTaskNode).Returns(1);
            buildEngine.Setup(be => be.LogErrorEvent(It.IsAny<BuildErrorEventArgs>()));

            task.BuildEngine = buildEngine.Object;
            task.SourceCodeFiles = new ITaskItem[] {
                new TaskItem { ItemSpec = file }
            };

            var success = task.Execute();

            Assert.AreEqual(expected, success);
        }


    }
}
