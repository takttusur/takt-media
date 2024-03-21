using TaktTusur.Media.BackgroundCrawling.Core.Interfaces;

namespace TaktTusur.Media.BackgroundCrawling.Core.Exceptions;

/// <summary>
/// Exception describes an error which is related with <see cref="IRepository{TEntity}"/>
/// </summary>
public abstract class RepositoryException : CoreException;