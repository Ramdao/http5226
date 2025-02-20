# Time Capsule
Time Capsule is a app where you can add timelines, like chapeters in a diary, and entires to that timeline, like content to that chapter. You can add, view, edit and delete timelines and entries. In timelines you can give it a name, description and name. for entries you can give it a name, description, location, images and which timeline it belongs to.

## Set up
> **Note:** This project was created in Visual studio Using ASP.NET core Web APP (Model-view Controller) using individual accounts template. Swagger was used to test the endpoints.


## End points used in this project
> /api/Entries/ListEntries
> /api/Entries/Find/{id}
> /api/Entties/Update/{id}
> /api/Entries/Add
> /api/Entries/Delete/{id}
>
> /api/Timeline/ListTimeline
> /api/Timeline/Find/{id}
> /api/Timeline/Update/{id}
> /api/Timeline/Add
> /api/Timeline/Delete/{id}
>
> /api/User/ListUser
> /api/User/Find/{id}
> /api/User/Update/{id}
> /api/User/Add
> /api/User/Delete/{id}
>
> /api/UserTimeline/LinkUserToTimeline
> /api/UserTimeline/UnlinkFromTimeline
> /api/UserTimeline/GetTimeLinesForuser/{userId}
> /api/UserTimeline/GetUsersForTImeline/{timelineId}

## Admin Page using views
1. Admin can add, view, edit and delete all users
2. Admin can add, link and unlink timelines from users to timelines
3. Admin can add, view, edit and delete all timelines
4. Admin can add, view edit and delete all entries

## User Page using views
1. Users can add, view, edit and delete timelines that they are linked to
2. Users can link and unlink themselves from timelines
3. Users can add, view, edit and delete entries to timelines that they are linked to


