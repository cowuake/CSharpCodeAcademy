using Xunit;
using Test2.Model;

namespace Test2.Test
{
    public class XTest
    {
        [Fact]
        public void Test1()
        {
            ItemOfExpenditure item = new ItemOfExpenditure((byte)ItemCategory.Travel, 500);
            item.AssignApproval();
            item.AssignRefund();
            Assert.Equal(LevelOfApproval.OperationalManager, item.Approval);
            Assert.Equal(550m, item.Refund);
        }

        [Fact]
        public void Test2()
        {
            ItemOfExpenditure item = new ItemOfExpenditure((byte)ItemCategory.Travel, 3100);
            item.AssignApproval();
            Assert.Equal(LevelOfApproval.None, item.Approval);
        }

        [Fact]
        public void Test3()
        {
            ItemOfExpenditure item = new ItemOfExpenditure((byte)ItemCategory.Board, 1400);
            item.AssignApproval();
            item.AssignRefund();
            Assert.Equal(LevelOfApproval.CEO, item.Approval);
            Assert.Equal(980m, item.Refund);
        }

        [Fact]
        public void Test4()
        {
            ItemOfExpenditure item = new ItemOfExpenditure((byte)ItemCategory.Generic, 50);
            item.AssignApproval();
            item.AssignRefund();
            Assert.Equal(LevelOfApproval.Manager, item.Approval);
            Assert.Equal(5, item.Refund);
        }

        [Fact]
        public void Test5()
        {
            ItemOfExpenditure item = new ItemOfExpenditure((byte)ItemCategory.Lodging, 350);
            item.AssignApproval();
            item.AssignRefund();
            Assert.Equal(LevelOfApproval.Manager, item.Approval);
            Assert.Equal(350, item.Refund);
        }
    }
}
