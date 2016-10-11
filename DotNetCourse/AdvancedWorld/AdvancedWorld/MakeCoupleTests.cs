using AdvancedWorld.HasName;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace AdvancedWorld.Tests
{
    [TestClass]
    public class MakeCoupleTests
    {
        [TestMethod]
        public void WrongCoupleWomanTest()
        {
            try
            {
                God god = new God();
                Girl girl = new Girl(NamesHelper.GenerateName(Sex.Woman));
                SmartGirl smartGirl = new SmartGirl(NamesHelper.GenerateName(Sex.Woman));
                god.MakeCouple(girl, smartGirl);
                Assert.Fail("No exception");
            } catch (Exception ex)
            {
                if (!(ex is WrongCoupleException))
                {
                    Assert.Fail("Not WrongCoupleException");
                }
            }
        }

        [TestMethod]
        public void WrongCoupleManTest()
        {
            try
            {
                God god = new God();
                Student student = new Student(NamesHelper.GenerateName(Sex.Man));
                Botan botan = new Botan(NamesHelper.GenerateName(Sex.Man));
                god.MakeCouple(student, botan);
                Assert.Fail("No exception");
            }
            catch (Exception ex)
            {
                if (!(ex is WrongCoupleException))
                {
                    Assert.Fail("Not WrongCoupleException");
                }
            }
        }

        [TestMethod]
        public void StudentGirlTest()
        {
            God god = new God(false);
            Student student = new Student("Антон");
            Girl girl = new Girl("Анна");
            IHasName result = god.MakeCouple(student, girl);

            Girl resultGirl = result as Girl;
            CheckGeneralGirlRequirements(resultGirl, girl.Name, student.Name + "овна");
        }

        [TestMethod]
        public void StudentPrettyGirlTest()
        {
            God god = new God(false);
            Student student = new Student("Антон");
            PrettyGirl girl = new PrettyGirl("Анна");
            IHasName result = god.MakeCouple(student, girl);

            Girl resultGirl = result as PrettyGirl;
            CheckGeneralGirlRequirements(resultGirl, girl.Name, student.Name + "овна");
        }

        [TestMethod]
        public void StudentSmartGirlTest()
        {
            God god = new God(false);
            Student student = new Student("Антон");
            SmartGirl girl = new SmartGirl("Анна");
            IHasName result = god.MakeCouple(student, girl);

            Girl resultGirl = result as Girl;
            CheckGeneralGirlRequirements(resultGirl, girl.Name, student.Name + "овна");
        }

        [TestMethod]
        public void BotanGirlTest()
        {
            God god = new God(false);
            Botan student = new Botan("Антон");
            Girl girl = new Girl("Анна");
            IHasName result = god.MakeCouple(student, girl);

            Girl resultGirl = result as SmartGirl;
            CheckGeneralGirlRequirements(resultGirl, girl.Name, student.Name + "овна");
        }

        [TestMethod]
        public void BotanPrettyGirlTest()
        {
            God god = new God(false);
            Botan student = new Botan("Антон");
            PrettyGirl girl = new PrettyGirl("Анна");
            IHasName result = god.MakeCouple(student, girl);

            Girl resultGirl = result as PrettyGirl;
            CheckGeneralGirlRequirements(resultGirl, girl.Name, student.Name + "овна");
        }

        [TestMethod]
        public void BotanSmartGirlTest()
        {
            God god = new God(false);
            Botan student = new Botan("Антон");
            SmartGirl girl = new SmartGirl("Анна");
            IHasName result = god.MakeCouple(student, girl);

            Book book = result as Book;
            if (book != null)
            {
                Assert.AreEqual(book.Name, girl.Name);
            }
            else
            {
                Assert.Fail("Incorrect resul type");
            }
        }

        private void CheckGeneralGirlRequirements(Girl girl, string expectedName, string expectedPatronymic)
        {
            if (girl != null)
            {
                Assert.AreEqual(girl.Name, expectedName);
                Assert.AreEqual(girl.Patronymic, expectedPatronymic);
                Assert.AreEqual(girl.Sex, Sex.Woman);
            }
            else
            {
                Assert.Fail("Incorrect resul type");
            }
        }
    }
}
