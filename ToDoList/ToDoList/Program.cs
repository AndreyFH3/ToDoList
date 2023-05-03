using Microsoft.EntityFrameworkCore;
using ToDoListLogic.DAL;

ApplicationContext cntx = new ApplicationContext();
Console.WriteLine(">>> Welcome To 'Just Do It'! <<<");

while (cntx != null)
{
    Console.WriteLine(">>> Enter What you want <<<\n" +
                  "\t1. - Get To Do List\n " +
                  "\t2. - Delete task\n" +
                  "\t3. - Add task\n" +
                  "\tAny other key. - Exit");

    string readedLine = Console.ReadLine();
    if (readedLine == "1")
    {
        cntx.Select();
        Console.WriteLine("\n\nEnter any key to back to start");
        Console.ReadKey();
    }
    else if (readedLine == "2")
    {
        Console.WriteLine(">>>Enter Id, you want to delete: ");
        if (int.TryParse(Console.ReadLine(), out int index))
        {
            cntx.DeleteData(index);
            Console.WriteLine("Deleted?");
        }
        else
        {
            Console.WriteLine("Id is not correct");
    
        }
        Console.WriteLine("\n\nEnter any key to back to start");
        Console.ReadKey();
    }
    else if(readedLine == "3")
    {
        todo todo = new todo();
        Console.WriteLine(">>>Enter Id: ");
        todo.ID = int.Parse(Console.ReadLine());
    
        Console.WriteLine(">>>Enter task name: ");
        todo.Name = Console.ReadLine();
    
        Console.WriteLine(">>>Enter Priority (1 - P1, 2 - P2, 3 - P3, 4 - P4): ");
        todo.Priority = int.Parse(Console.ReadLine());
    
        Console.WriteLine(">>>Enter description: ");
        todo.Text = Console.ReadLine();
    
        cntx.AddElement(todo);
        Console.WriteLine("\n\nEnter any key to back to start");
        Console.ReadKey();
    }
    else 
    {
        cntx = null;
    }
    Console.Clear();
}