# TAKT Events API

Сервис предоставляет информацию о новосятх клуба, событиях, в перспективе - событиях туристско-альпинисткого сообщества Томска. 

## Технологии

* .NET
* Swagger
* PostgreSQL
* REST API
* VK API

## Способ сборки

## APIv1 методы
Prefix: `host/api/v1/`

| Method | Route | Request | Response | Notes |
|--------|-------|---------|----------|-------|
| GET  | events/ | groups:EventGroup[] - фильтр по группам, skip - сколько записей пропустить, take - сколько записей взять | `{events: ClubEvent[] , totalCount: Integer}` | Возвращает отсортированный массив событий открытых для публичного посещения/участия, сначала идут события который были недавно или скоро будут `Math.Min(Math.Abs(DateTime.Now - ClubEvent.startDate), Math.Abs(DateTime.Now - ClubEvent.endDate)` |
|GET | news/ | skip, take | `{news: ClubNews[], totalCount: integer}` | Возвращает актуальные новости клуба, импоритированные со страниц в VK |

## APIv1 объекты

### ClubEvent

Объект описывает событие, мероприятие, соревнования и т.д.
```ts
class ClubEvent {
  id: number, // Идентификатор события в БД
  title: string, // Название события
  description: string, // Описание события
  sourceId: number, // Идентификатор источника, откуда получено событие
  sourceTitle: string, // Название источника, откуда получено событие
  sourceUrl: string | null, // Ссылка на источник события, обратите внимание, это не ссылка на пост или группу события, а ссылка на организаторов
  url: string | null, // Ссылка на пост или группу мероприятия, если она есть
  bannerUrl: string | null // Ссылка на картинку для события
  startDate: DateTime, // Начало события
  endDate: DateTime | null // Конец события, если известно
}
```

### EventGroup

Группа события или источник события. Нужно для указания, для какого круга лиц данное событие(общее, внутреннее или организатор - другое сообщество)

```ts
enum EventGroup {
  Unknown = 0,
  Public = 1, // Мероприятие проводи наш клуб, возможно участие людей не из клуба
  Internal = 2, // Внутреннее мероприятие, участствовать могут только члены клуба, или другие люди по согласованию
  External = 3 // Событие, которое организует не клуб, например ТФА, ТФСТ, ФАиС ТО и т.д.
}
```

### ClubNews

Новость

```ts
class ClubNews {
  id: number,
  title: string,
  sourceId: number,
  sourceTitle: string,
// ????
}
```
