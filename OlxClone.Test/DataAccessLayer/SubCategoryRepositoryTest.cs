namespace OlxClone.Test.DataAccessLayer;
public class SubCategoryRepositoryTest
{
    private DbContextOptions<AppDbContext> options =
        new DbContextOptionsBuilder<AppDbContext>()
        .UseInMemoryDatabase("OlxClone")
        .Options;
    private AppDbContext _dbContext;
    private SubCategoryRepository subcategoryRepository;
    [SetUp]
    public void Setup()
    {
        _dbContext = new AppDbContext(options);
        _dbContext.Database.EnsureCreated();
      subcategoryRepository = new SubCategoryRepository(_dbContext);
    }

    [Test]
    public async Task AddAsync_AddNewSubcategoryToDatabase()
    {
        SubCategory category = new() 
        { 
            Name = "Test", 
            CategoryId = 1 
        };
        await subcategoryRepository.AddAsync(category);
        await _dbContext.SaveChangesAsync();

        Assert.That(_dbContext.SubCategories.Count(), Is.EqualTo(1));
    }

    [Test]
    public async Task GetAllAsync_ReturnAllCategories()
    {
        var categories = await subcategoryRepository.GetAllAsync();
        Assert.That(categories.Count(), Is.EqualTo(1));
    }

    [Test]
    public async Task GetByIdAsync_ReturnSubcategoryById()
    {
        var category = await subcategoryRepository.GetByIdAsync(1);
        Assert.That(category!.Name, Is.EqualTo("Test"));
    }

    [Test]
    public async Task UpdateAsync_UpdateSubcategory()
    {
        var subcategory = await subcategoryRepository.GetByIdAsync(1);
        subcategory!.Name = "Test2";
        subcategory.CategoryId = 2;
        await subcategoryRepository.UpdateAsync(subcategory);
        await _dbContext.SaveChangesAsync();

        Assert.That(_dbContext.SubCategories.First().Name,
                    Is.EqualTo("Test2"));
    }

    [Test]
    public async Task DeleteAsync_DeleteSubcategory()
    {
        SubCategory category = new() { Name = "Test123" };
        await subcategoryRepository.AddAsync(category);
        await _dbContext.SaveChangesAsync();

        await subcategoryRepository.DeleteAsync(category);
        await _dbContext.SaveChangesAsync();

        Assert.That(_dbContext.Categories.Count(), Is.EqualTo(1));
    }
}