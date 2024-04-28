namespace OlxClone.Test.DataAccessLayer;
public class CategoryRepositoryTest
{
    private DbContextOptions<AppDbContext> options = 
        new DbContextOptionsBuilder<AppDbContext>()
        .UseInMemoryDatabase("OlxClone")
        .Options;
    private AppDbContext _dbContext;
    private CategoryRepository categoryRepository;
    [SetUp]
    public void Setup()
    {
        _dbContext = new AppDbContext(options);
        _dbContext.Database.EnsureCreated();
        categoryRepository = new CategoryRepository(_dbContext);
    }

    [Test]
    public async Task AddAsync_AddNewCategoryToDatabase()
    {
        Category category = new() { Name = "Test" };
        await categoryRepository.AddAsync(category);
        await _dbContext.SaveChangesAsync();

        Assert.That(_dbContext.Categories.Count(), Is.EqualTo(1));
    }

    [Test]
    public async Task GetAllAsync_ReturnAllCategories()
    {
        var categories = await categoryRepository.GetAllAsync();
        Assert.That(categories.Count(), Is.EqualTo(1));
    }

    [Test]
    public async Task GetByIdAsync_ReturnCategoryById()
    {
        var category = await categoryRepository.GetByIdAsync(1);
        Assert.That(category!.Name, Is.EqualTo("Test"));
    }

    [Test]
    public async Task UpdateAsync_UpdateCategory()
    {
        var category = await categoryRepository.GetByIdAsync(1);
        category!.Name = "Test2";
        await categoryRepository.UpdateAsync(category);
        await _dbContext.SaveChangesAsync();

        Assert.That(_dbContext.Categories.First().Name, 
                    Is.EqualTo("Test2"));
    }

    [Test]
    public async Task DeleteAsync_DeleteCategory()
    {
        Category category = new() { Name = "Test123" };
        await categoryRepository.AddAsync(category);
        await _dbContext.SaveChangesAsync();

        await categoryRepository.DeleteAsync(category);
        await _dbContext.SaveChangesAsync();

        Assert.That(_dbContext.Categories.Count(), Is.EqualTo(1));
    }
}