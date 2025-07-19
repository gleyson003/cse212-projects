using Microsoft.VisualStudio.TestTools.UnitTesting;

// TODO Problem 2 - Write and run test cases and fix the code to match requirements.

[TestClass]
public class PriorityQueueTests
{
    [TestMethod]
    // Scenario: Insert 3 items with different priorities: A(1), B(5), C(3). Remove all.
    // Expected Result: Items removed in order: B, C, A (highest priority first)
    // Defect(s) Found: Original code returned wrong value and did not remove the item from queue.
    public void TestPriorityQueue_DifferentPriorities()
    {
        var queue = new PriorityQueue();
        queue.Enqueue("A", 1);
        queue.Enqueue("B", 5);
        queue.Enqueue("C", 3);

        Assert.AreEqual("B", queue.Dequeue());
        Assert.AreEqual("C", queue.Dequeue());
        Assert.AreEqual("A", queue.Dequeue());
    }

    [TestMethod]
    // Scenario: Insert multiple items with same priority: A(3), B(3), C(3). Remove all.
    // Expected Result: Items removed in order: A, B, C (FIFO tie-breaker)
    // Defect(s) Found: Original code incorrectly used ">=" which made it return last-in-first-out on equal priority.
    public void TestPriorityQueue_SamePriorityFIFO()
    {
        var queue = new PriorityQueue();
        queue.Enqueue("A", 3);
        queue.Enqueue("B", 3);
        queue.Enqueue("C", 3);

        Assert.AreEqual("A", queue.Dequeue());
        Assert.AreEqual("B", queue.Dequeue());
        Assert.AreEqual("C", queue.Dequeue());
    }

    [TestMethod]
    // Scenario: Try to dequeue from an empty queue
    // Expected Result: Should throw InvalidOperationException with specific message
    // Defect(s) Found: No exception was thrown or wrong exception/message.
    public void TestPriorityQueue_EmptyQueue()
    {
        var queue = new PriorityQueue();

        try
        {
            queue.Dequeue();
            Assert.Fail("Exception should have been thrown.");
        }
        catch (InvalidOperationException e)
        {
            Assert.AreEqual("The queue is empty.", e.Message);
        }
    }

    [TestMethod]
    // Scenario: Insert items: A(2), B(5), C(5), D(1). Remove all.
    // Expected Result: Remove B (first with highest), then C, A, D
    // Defect(s) Found: Original code prioritized the last with max priority due to ">=".
    public void TestPriorityQueue_MultipleWithSameHighest()
    {
        var queue = new PriorityQueue();
        queue.Enqueue("A", 2);
        queue.Enqueue("B", 5);
        queue.Enqueue("C", 5);
        queue.Enqueue("D", 1);

        Assert.AreEqual("B", queue.Dequeue());
        Assert.AreEqual("C", queue.Dequeue());
        Assert.AreEqual("A", queue.Dequeue());
        Assert.AreEqual("D", queue.Dequeue());
    }
}