using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace NorthwindEmployeeEfCoreService;

/// <summary>
/// A service for interacting with the "Employees" table using Entity Framework Core.
/// </summary>
public sealed class EmployeeEfCoreService
{
    private readonly IDbContextFactory<EmployeeContext> _dbContextFactory;
    /// <summary>
    /// Initializes a new instance of the <see cref="EmployeeEfCoreService"/> class.
    /// </summary>
    /// <param name="dbContextFactory">The database context factory used to create database context instances.</param>
    /// <exception cref="ArgumentNullException">Thrown when either <paramref name="dbContextFactory"/> is null.</exception>
    public EmployeeEfCoreService(IDbContextFactory<EmployeeContext> dbContextFactory)
    {
        if (dbContextFactory == null)
        {
            throw new ArgumentNullException(nameof(dbContextFactory), "Database context factory cannot be null.");
        }

        _dbContextFactory = dbContextFactory;
    }

    /// <summary>
    /// Retrieves a list of all employees from the Employees table of the database.
    /// </summary>
    /// <returns>A list of Employee objects representing the retrieved employees.</returns>
    public IList<Employee> GetEmployees()
    {
        using var dbContext = _dbContextFactory.CreateDbContext();
        return dbContext.Employees.ToList();
    }

    /// <summary>
    /// Retrieves an employee with the specified employee Id.
    /// </summary>
    /// <param name="id">The id of the employee to retrieve.</param>
    /// <returns>The retrieved an <see cref="Employee"/> instance.</returns>
    /// <exception cref="EmployeeServiceException">Thrown when the employee is not found.</exception>
    public Employee GetEmployee(long id)
    {
        using var dbContext = _dbContextFactory.CreateDbContext();
        var employee = dbContext.Employees.Find(id);
        if (employee == null)
        {
            throw new EmployeeServiceException($"Employee with ID {id} not found.");
        }
        return employee;
    }

    /// <summary>
    /// Adds a new employee to Employee table of the database.
    /// </summary>
    /// <param name="employee">The <see cref="Employee"/> object containing the employee's information.</param>
    /// <returns>The id of the newly added employee.</returns>
    /// <exception cref="EmployeeServiceException">Thrown when an error occurs while adding the employee.</exception>
    public long AddEmployee(Employee employee)
    {
        using var dbContext = _dbContextFactory.CreateDbContext();
        dbContext.Employees.Add(employee);
        dbContext.SaveChanges();
        return employee.Id;
    }

    /// <summary>
    /// Removes an employee from the the Employee table of the database based on the provided employee Id.
    /// </summary>
    /// <param name="id">The ID of the employee to remove.</param>
    /// <exception cref="EmployeeServiceException"> Thrown when an error occurs while attempting to remove the employee.</exception>
    public void RemoveEmployee(long id)
    {
        using var dbContext = _dbContextFactory.CreateDbContext();
        var employee = dbContext.Employees.Find(id);
        if (employee == null)
        {
            throw new EmployeeServiceException($"Employee with ID {id} not found.");
        }
        dbContext.Employees.Remove(employee);
        dbContext.SaveChanges();
    }

    /// <summary>
    /// Updates an employee record in the Employee table of the database.
    /// </summary>
    /// <param name="employee">The employee object containing updated information.</param>
    /// <exception cref="EmployeeServiceException">Thrown when there is an issue updating the employee record.</exception>
    public void UpdateEmployee(Employee employee)
    {
        using var dbContext = _dbContextFactory.CreateDbContext();
        var existingEmployee = dbContext.Employees.Find(employee.Id);
        if (existingEmployee == null)
        {
            throw new EmployeeServiceException($"Employee with ID {employee.Id} not found.");
        }

        existingEmployee.FirstName = employee.FirstName;
        existingEmployee.LastName = employee.LastName;
        existingEmployee.Title = employee.Title;
        existingEmployee.TitleOfCourtesy = employee.TitleOfCourtesy;
        existingEmployee.BirthDate = employee.BirthDate;
        existingEmployee.HireDate = employee.HireDate;
        existingEmployee.Address = employee.Address;
        existingEmployee.City = employee.City;
        existingEmployee.Region = employee.Region;
        existingEmployee.PostalCode = employee.PostalCode;
        existingEmployee.Country = employee.Country;
        existingEmployee.HomePhone = employee.HomePhone;
        existingEmployee.Extension = employee.Extension;
        existingEmployee.Notes = employee.Notes;
        existingEmployee.ReportsTo = employee.ReportsTo;
        existingEmployee.PhotoPath = employee.PhotoPath;

        dbContext.SaveChanges();
    }
}