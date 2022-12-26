/// <summary>
/// Gets {listPropertyName} in {entityTypeName}.
/// </summary>
/// <param name="dbContext">The database context.</param>
/// <param name="{primaryKeyVarName}">The {entityTypeName} key to get</param>
/// <param name="skip">How many {propertyName} to skip.</param>
/// <param name="take">How many {propertyName} to take. (Default = 50)</param>
/// <param name="orderDir">Optional Order direction</param>
/// <param name="orderBy">Optional order by predicate.</param>
/// <returns>A bool if there's at least one record found and the resulting array. </returns>
public static (bool, {returnTypeFullName}[]) Get{entityTypeName}{listPropertyName}(this {contextFullName} dbContext, {primaryKeyFullName} {primaryKeyVarName},
        int skip = 0, int take = 50, int orderDir = 1, 
        Expression<Func<{returnTypeFullName}, bool>> orderBy = null)
{
    var {entityObjectVarName} = dbContext.{propertyName}.FirstOrDefault(d => d.{primaryKeyPropertyName} == {primaryKeyVarName});

    if ({entityObjectVarName} == null) return (false, null);
    
    var currentTake = take;
    if (take > 200) currentTake = 200;

    var query = dbContext
        .{propertyName}
        .Where(data => data.{primaryKeyPropertyName} == {primaryKeyVarName})
        .SelectMany(o => o.{listPropertyName})
        .Select(x => new {returnTypeFullName}(x))
        .Skip(skip)
        .Take(currentTake);
        
    if(orderBy != null) query = orderDir == 1 ? query.OrderBy(orderBy) : query.OrderByDescending(orderBy);

    return (query.Any(), query.ToArray());
}