using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Build.Framework;

namespace BomSquad.BuildTasks
{
    public class BomSquadTask: Microsoft.Build.Utilities.Task
    {
        [Required]
        public bool ShouldHaveBom { get; set; }


        public ITaskItem[] SourceCodeFiles
        {
            get;
            set;
        }

        public BomSquadTask()
        {
            // by default, the files searched SHOULD have a BOM. Mark as false in order to ensure that
            // files do not have a BOM. 
           this.ShouldHaveBom = true;
        }

        public override bool Execute()
        {
            List<string> sourceCodeFiles = new List<string>();
            if (this.SourceCodeFiles != null)
            {
                sourceCodeFiles.AddRange(this.SourceCodeFiles.Select(it => it.ItemSpec));
            }

            var dog = new SnifferDog();
            var failed = false;
            foreach (var sourceCodeFile in sourceCodeFiles)
            {
                Log.LogMessageFromText("Checking " + sourceCodeFile, MessageImportance.High);
                var asExpected = dog.DetectBom(sourceCodeFile) == this.ShouldHaveBom;
                if (!asExpected)
                {
                    Log.LogError("'" + sourceCodeFile + "' has the wrong kind of BOM");
                    failed = true;
                }
            }

            return !failed;
        }
    }
}
