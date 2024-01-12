namespace TaktTusur.Media.Clients.VkApi;

public class VkGroupInfo
{
    #region Base

    /// <summary>
    /// Возвращает и задает Id сообщества.
    /// </summary>
    public int GroupId { get; set; }

    /// <summary>
    /// Возвращает и задает название сообщества.
    /// </summary>
    public string GroupName { get; set; }

    /// <summary>
    /// Возвращает и задает короткий адрес записи.
    /// </summary>
    public string GroupScreenName { get; set; }

    /// <summary>
    /// Возвращает и задает является ли сообщество закрытым.
    /// </summary>
    public int GroupIsClosed { get; set; }

    /// <summary>
    /// Возвращает и задает тип сообщества.
    /// </summary>
    public string GroupType { get; set; }

    /// <summary>
    /// Возвращает и задает URL главной фотографии с размером 50x50px.
    /// </summary>
    public string GroupPhoto50 { get; set; }

    /// <summary>
    /// Возвращает и задает URL главной фотографии с размером 100x100px.
    /// </summary>
    public string GroupPhoto100 { get; set; }

    /// <summary>
    /// Возвращает и задает URL главной фотографии в максимальном размере.
    /// </summary>
    public string GroupPhoto200 { get; set; }

    #endregion

    #region Optional

    /// <summary>
    /// Возвращает и задает дату начала события.
    /// </summary>
    public int StartDateTime { get; set; }

    /// <summary>
    /// Возвращает и задает дату окончания события.
    /// </summary>
    public int FinishDateTime { get; set; }

    /// <summary>
    /// Возвращает и задает описание группы.
    /// </summary>
    public string Description { get; set; }

    #endregion
}