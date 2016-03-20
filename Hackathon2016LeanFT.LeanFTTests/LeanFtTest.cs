using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HP.LFT.SDK;
using HP.LFT.Verifications;
using HP.LFT.SDK.Web;

namespace Hackathon2016LeanFT.LeanFTTests
{
    [TestClass]
    public class LeanFtTest : UnitTestClassBase<LeanFtTest>
    {
        IBrowser browser;
        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            GlobalSetup(context);
        }

        [TestInitialize]
        public void TestInitialize()
        {
            browser = BrowserFactory.Launch(BrowserType.InternetExplorer);
        }

        [TestMethod]
        public void TestMethod1()
        {
            try
            {
                //Set reporter to take all snapshots
                Reporter.SnapshotCaptureLevel = HP.LFT.Report.CaptureLevel.All;

                //Navigate to Search Engine
                browser.Navigate("www.duckduckgo.com");

                //Search for Learn2Automate blog
                IEditField txtSearch = browser.Describe<IEditField>(new EditFieldDescription
                {
                    Type = "text",
                    TagName = "INPUT",
                    Name = "q"
                });
                txtSearch.SetValue("learn2automate");

                IButton btnSearch = browser.Describe<IButton>(new ButtonDescription
                {
                    ButtonType = "submit",
                    TagName = "INPUT",
                    Name = "S"
                });
                btnSearch.Click();


                //Select the first search result
                ILink firstResult = browser.Describe<ILink>(new LinkDescription
                {
                    TagName = "A",
                    InnerText = As.RegExp(".*learn2automate.*"),
                    Index = 0
                });

                Assert.IsTrue(firstResult.Exists(10));

                firstResult.Click();

                //Search for LeanFT if the blog opens
                IEditField searchBox = browser.Describe<IEditField>(new EditFieldDescription
                {
                    Type = "text",
                    TagName = "INPUT",
                    Name = "s"
                });

                Assert.IsTrue(searchBox.Exists(10));

                searchBox.SetValue("leanft");

                IButton goButton = browser.Describe<IButton>(new ButtonDescription
                {
                    ButtonType = "submit",
                    TagName = "INPUT",
                    Name = "Go"
                });

                goButton.Click();

                //Verify that the blog entry with title LeanFT opens
                ILink blogResult = browser.Describe<ILink>(new LinkDescription
                {
                    TagName = "A",
                    InnerText = As.RegExp(".*leanfT.*"),
                    Index = 0
                });

                Assert.IsTrue(blogResult.Exists(10));
                blogResult.Highlight();
            }
            catch (Exception e)
            {
                Reporter.ReportEvent("Look for LeanFT on Lean2Automate", e.Message);
            }
        }

        [TestCleanup]
        public void TestCleanup()
        {
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            GlobalTearDown();
        }
    }
}
