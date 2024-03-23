using TaktTusur.Media.Clients.VkApi.Models.Enums;

namespace TaktTusur.Media.Clients.VkApi.Models;

public class VkGroupInfo
{
    #region Base

    /// <summary>
    /// Идентификатор сообщества.
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Название сообщества.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Короткий адрес.
    /// </summary>
    public string ScreenName { get; set; }

    /// <summary>
    /// Тип доступности сообщества. 
    /// (Accessible - открытая, Inaccessible - закрытая, Private - частная)
    /// </summary>
    public AccessTypes AccessType { get; set; }

    /// <summary>
    /// Тип сообщества.
    /// Group - группа, Page - публичная страница, Event - мероприятие
    /// </summary>
    public VkGroupTypes Type { get; set; }

    /// <summary>
    /// URL главной фотографии с размером 50x50px.
    /// </summary>
    public string Photo50 { get; set; }

    /// <summary>
    /// URL главной фотографии с размером 100x100px.
    /// </summary>
    public string Photo100 { get; set; }

    /// <summary>
    /// URL главной фотографии в максимальном размере.
    /// </summary>
    public string Photo200 { get; set; }
    
    /// <summary>
    /// URL сообщества.
    /// </summary>
    public string URL { get; set; }

    #endregion

    #region Optional

    /// <summary>
    /// Для встреч - время начала события. Для публичных страниц - дата основания.
    /// </summary>
    public DateTimeOffset? StartDateTime { get; set; }

    /// <summary>
    /// Дата окончания встречи.
    /// </summary>
    public DateTimeOffset? FinishDateTime { get; set; }

    /// <summary>
    /// Тескт описания сообщества.
    /// </summary>
    public string Description { get; set; }
    
    #endregion
}