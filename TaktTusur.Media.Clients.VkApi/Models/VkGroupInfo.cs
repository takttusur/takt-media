namespace TaktTusur.Media.Clients.VkApi.Models;

public class VkGroupInfo
{
    #region Base

    /// <summary>
    /// Возвращает и задает Id сообщества.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Возвращает и задает название сообщества.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Возвращает и задает короткий адрес записи.
    /// </summary>
    public string ScreenName { get; set; }

    /// <summary>
    /// Возвращает и задает является ли сообщество закрытым.
    /// </summary>
    public int IsClosed { get; set; }

    /// <summary>
    /// Возвращает и задает тип сообщества.
    /// </summary>
    public string Type { get; set; }

    /// <summary>
    /// Возвращает и задает URL главной фотографии с размером 50x50px.
    /// </summary>
    public string Photo50 { get; set; }

    /// <summary>
    /// Возвращает и задает URL главной фотографии с размером 100x100px.
    /// </summary>
    public string Photo100 { get; set; }

    /// <summary>
    /// Возвращает и задает URL главной фотографии в максимальном размере.
    /// </summary>
    public string Photo200 { get; set; }
    
    /// <summary>
    /// URL группы.
    /// </summary>
    public string URL { get; set; }

    #endregion

    #region Optional

    /// <summary>
    /// Возвращает и задает дату начала события.
    /// </summary>
    public DateTimeOffset? StartDateTime { get; set; }

    /// <summary>
    /// Возвращает и задает дату окончания события.
    /// </summary>
    public DateTimeOffset? FinishDateTime { get; set; }

    /// <summary>
    /// Возвращает и задает описание группы.
    /// </summary>
    public string Description { get; set; }
    
    #endregion
}