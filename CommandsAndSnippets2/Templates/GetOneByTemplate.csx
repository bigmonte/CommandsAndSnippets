/// <summary>
/// Gets the first result in the table, and returns a projection of <see cref="{returnTypeFullName}"></see>
/// </summary>
/// <param name="dbContext">The database context.</param>
/// <param name="{byParamVarName}">By {byParamPropertyName} parameter type.</param>
/// <returns>A projection of {entityTypeName}> </returns>
public static (bool, {returnTypeFullName}) GetOne{entityTypeName}By{byParamPropertyName} (this {contextFullName} dbContext, {byParamFullType} {byParamVarName}) 
{
    var {findEntityVarName} = dbContext.{propertyName}
        .Where(x => x.{byParamPropertyName} == {byParamVarName})
        .Select(x => new {returnTypeFullName}(x))
        .FirstOrDefault();

    return ({findEntityVarName} != null, {findEntityVarName});
}