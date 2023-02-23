public class WorkFlowEngine
{
    private readonly IList<IWorkFlow> workFlows;
    public void RegisterWorkFlows(IWorkFlow workFlow)
    {
        workFlows.Add(workFlow);
    }

    public WorkFlowEngine()
    {
        workFlows = new List<IWorkFlow>();
    }
    public void Run(Workflow flow)
    {
        foreach (var flo in workFlows)
            flo.Execute(new Workflow());
    }
}

public interface IWorkFlow
{
    public void Execute(Workflow flow);

}
public class GreatWorkFlow : IWorkFlow
{
    public void Execute(Workflow flow)
    {
        Console.WriteLine("Great results and great well being");
    }
}

public class Workflow : IWorkFlow
{
    public void Execute(Workflow flow)
    {
        Console.WriteLine("Average results and average wellbeing");
    }
}


// Main 
/* var engine = new WorkFlowEngine();
   engine.RegisterWorkFlows(new GreatWorkFlow());
   engine.RegisterWorkFlows(new Workflow());
   engine.Run(new Workflow()); */

