using TaktTusur.Media.Core.Interfaces;

namespace TaktTusur.Media.Core.Exceptions;

/// <summary>
/// Exception describes an error which is related with <see cref="IRepository{TEntity}"/>
/// </summary>
public abstract class RepositoryException : CoreException;