/// <summary>
/// Gets {propertyName}.
/// </summary>
/// <param name="dbContext">The database context.</param>
/// <param name="skip">How many {propertyName} to skip.</param>
/// <param name="take">How many {propertyName} to take. (Default = 50)</param>
/// <param name="orderDir">Optional Order direction</param>
/// <param name="where">Optional where predicate.</param>
/// <param name="orderBy">Optional order by predicate.</param>
/// <returns>A bool if the result is successful and a projection of the first occurrence of {propertyName}. </returns>
public static (bool, System.Collections.Generic.List<{returnTypeFullName}>) Get{propertyName}(this {contextFullName} dbContext, int skip = 0, int take = 50, int orderDir = 1)
{
    if (take > 200) take = 200;

    var query =  dbContext
                 .{propertyName}
                 .Skip(skip)
                 .Take(take)
                 .Select(x => new {returnTypeFullName}(x));

                 
    var result = query.ToList();
    
    return (result.Any(), result);
}