using BusinessLogicLayer.Extended;
using DTO.DTOs.CategoryDtos;
using Moq;

namespace OlxClone.Test.BusinessLogicLayer;
public class CategoryServiceTest
{
    private DbContextOptions<AppDbContext> options =
        new DbContextOptionsBuilder<AppDbContext>()
        .UseInMemoryDatabase("OlxClone1")
        .Options;
    private AppDbContext _dbContext;
    private IMapper _mapper;
    private IUnitOfWork _unitOfWork;
    private CategoryService _categoryService;
    [SetUp]
    public void Setup()
    {
        _dbContext = new AppDbContext(options);
        _mapper = new MapperConfiguration(mc =>
        {
            mc.AddProfile(new AutoMepperProfile());
        }).CreateMapper();
        _unitOfWork = new UnitOfWork(_dbContext);
        _categoryService = new CategoryService(_unitOfWork, _mapper);
    }

    [Test]
    public void AddCategory_Test1_DtoIsNull_ThrowArgumentNullException()
    {
        var unitOfWork = new Mock<IUnitOfWork>();
        var mapper = new Mock<IMapper>();
        var categoryService = new CategoryService(unitOfWork.Object, mapper.Object);

        //setup
        AddCategoryDto categoryDto = null;

        //Assert
        Assert.ThrowsAsync<ArgumentNullException>(async ()
                                  => await categoryService.Add(categoryDto));

        unitOfWork.Verify();
    }

    [Test]
    public async Task AddCategory_Test2_IsValid_Added()
    {
        // Arrange
        var unitOfWork = new Mock<IUnitOfWork>();
        var mapper = new Mock<IMapper>();
        var categoryService = new CategoryService(unitOfWork.Object, mapper.Object);

        // Set up mapper to map the categoryDto to a Category object
        mapper.Setup(x => x.Map<Category>(It.IsAny<AddCategoryDto>()))
              .Returns((AddCategoryDto dto) => new Category { Name = dto.Name });

        unitOfWork.Setup(x => x.CategoryInterface.AddAsync(It.IsAny<Category>()))
                  .Returns(Task.CompletedTask);

        // Arrange
        var categoryDto = new AddCategoryDto
        {
            Name = "Test234"
        };

        // Act
        await categoryService.Add(categoryDto);

        // Assert
        unitOfWork.Verify(x => x.CategoryInterface.AddAsync(It.IsAny<Category>()), Times.Once);
        mapper.Verify(x => x.Map<Category>(categoryDto), Times.Once);
    }


    [Test]
    public void AddCategory_Test3_IsExist_ThrowCustomException()
    {
        //Arrange
        var categoryDto = new AddCategoryDto
        {
            Name = "Test"
        };

        var unitOfWork = new Mock<IUnitOfWork>();
        var mapper = new Mock<IMapper>();
        var categoryService = new CategoryService(unitOfWork.Object, mapper.Object);

        unitOfWork.Setup(x => x.CategoryInterface.GetAllAsync())
                  .ReturnsAsync(new List<Category> { new Category { Name = "Test" } });

        //Assert
        Assert.ThrowsAsync<CustomException>(async () => await categoryService.Add(categoryDto));
    }

    [Test]
    public async Task GetAllAsync_Test4_ReturnsAllCategories()
    {
        //Act
        var categories = await _categoryService.GetAll();

        //Assert
        Assert.That(categories.Count, Is.EqualTo(1));
    }

    [Test]
    public async Task GetByIdAsync_Test5_ReturnsCategory()
    {
        //Act
        var category = await _categoryService.GetById(1);

        //Assert
        Assert.That(category.Name, Is.EqualTo("Test"));
    }

    [Test]
    public void GetByIdAsync_Test6_ThrowArgumentNullException()
    {
        //Assert
        Assert.ThrowsAsync<ArgumentNullException>(async ()
                       => await _categoryService.GetById(2));
    }
}