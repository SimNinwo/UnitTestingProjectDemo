using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyClasses;
using System.Configuration;
using System.IO;

namespace MyClassesTest
{
    [TestClass]
    public class FileProcessTest
    {
        private const string BAD_FILE_NAME = @"C:\FileBadName.txt"; //use constants
        private string _goodFileName;

        [TestMethod]
        public void FileNameDoesExist()
        {
            FileProcess fp = new FileProcess();
            bool fromCall;

            // SET-UP
            SetGoodFileName();
            File.AppendAllText(_goodFileName, "Some Text");
            fromCall = fp.FileExists(_goodFileName);
            
            // TEAR-DOWN
            File.Delete(_goodFileName);

            Assert.IsTrue(fromCall);
        }

        [TestMethod]
        public void FileNameDoesNotExist()
        {
            FileProcess fp = new FileProcess();
            bool fromCall;

            fromCall = fp.FileExists(BAD_FILE_NAME);

            Assert.IsFalse(fromCall);
        }

        // supporting filenaem Test method
        public void SetGoodFileName()
        {
            _goodFileName = ConfigurationManager.AppSettings["GoodFileName"];

            if (_goodFileName.Contains("[AppPath]"))
            {
                _goodFileName = _goodFileName.Replace("[AppPath]",
                    Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void FileNameNullOrEmpty_ThrowsArgumentNullException()
        {
            FileProcess fp = new FileProcess();

            fp.FileExists("");
        }

        [TestMethod]
        public void FileNameNullOrEmpty_ThrowsArgumentNullException_UsingTryCatch()
        {
            FileProcess fp = new FileProcess();

            try
            {
                fp.FileExists("");
            }
            catch (ArgumentNullException e)
            {
                // Test was a success
                return;
            }

            Assert.Fail("Call to FileExist did NOT throw an ArgumentNullException.");
        }


    }
}
