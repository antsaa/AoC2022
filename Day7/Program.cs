var input = System.IO.File.ReadAllLines("input.txt");


var root = new Folder() { Name = "/" };
var directories = new List<Folder> { root };

void PopulateDirectories()
{
    var current = root;
    
    foreach (var line in input)
    {
        if (line.StartsWith("dir"))
        {
            var folder = new Folder { Name = line.Replace("dir ", ""), Parent = current };
            current.SubFolders.Add(folder);
            directories.Add(folder);
        }
        else if (char.IsDigit(line[0]))
        {
            var size = int.Parse(line.Split(' ')[0]);
            current.Size += size;

        }
        else if (line == "$ cd ..")
        {
            current = current.Parent;
        }
        else if (line == "$ cd /")
        {
            current = root;
        }
        else if (line.StartsWith("$ cd"))
        {
            var destFolder = line.Replace("$ cd ", "");
            current = current.SubFolders.Single(x => x.Name == destFolder);
        }
    }
}

void Part1()
{
    var folders = directories.Select(CalculateDirectorySize).Where(x => x <= 100_000).Sum();
    Console.WriteLine(folders);
}

void Part2()
{
    const int totalCapacity = 70000000;
    const int spaceRequired = 30000000;
    var spaceUsed = CalculateDirectorySize(root);
    var spaceRemaining = totalCapacity - spaceUsed;

    var (smallestDir, size) = directories
        .Select(d => (Directory: d, Size: CalculateDirectorySize(d)))
        .OrderByDescending(x => spaceRemaining + x.Size >= spaceRequired)
        .ThenBy(x => x.Size)
        .FirstOrDefault();

    Console.WriteLine(size);
}

long CalculateDirectorySize(Folder folder)
{
    if (!folder.SubFolders.Any())
    {
        return folder.Size;
    }

    return folder.Size + folder.SubFolders.Sum(CalculateDirectorySize);
}

PopulateDirectories();
Part1();
Part2();


class Folder
{
    public string Name { get; set; }
    public List<Folder> SubFolders { get; set; } = new List<Folder>();
    public int Size { get; set; } = 0;
    public Folder? Parent { get; set; }
}
