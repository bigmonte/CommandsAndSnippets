/// <summary>
/// Gets the first result following the query with a expression.
/// </summary>
/// <param name="dbContext">The database context.</param>
/// <param name="with">FirstOrDefault argument to pass.</param>
/// <returns>A bool if the first occurrence of {returnTypeName} is returned.</returns>
public static (bool, {returnTypeFullName}) Get{entityTypeName}With (this {contextFullName} dbContext, 
       Expression<Func<{returnTypeFullName},bool>> with) 
{
    var {findEntityVarName} =
        dbContext.{propertyName}
        .Select(x => new {returnTypeFullName}(x))
        .FirstOrDefault(with);

    return ({findEntityVarName} != null, {findEntityVarName});

}