using System.Collections.Generic;

public class ItemSerialization : Singleton
{
    private List<Container> items = new List<Container>();

	#region PRIVATE FUNCTIONS
	
	
	
	#endregion
	
	#region PUBLIC FUNCTIONS
	
	public void CreateNewContainer(string name, string path)
    {
        Container container = new Container();
        container.Init(name, path);
        items.Add(container);
    }

    public void NewList() { items = new List<Container>(); }

    public List<Container> GetContainers() { return items; }
	
	#endregion

    [System.Serializable]
    public class Container
    {
        private string name;
        private string spritePath;

        public void Init(string name, string spritePath)
        {
            this.name = name;
            this.spritePath = spritePath;
        }

        public string GetName() { return name; }

        public string GetIconPath() { return spritePath; }
    }
}
