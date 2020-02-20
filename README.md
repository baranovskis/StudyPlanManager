## TODO :fire:
- [x] Check if web server is already running.
- [x] Create & connect to a local database.
- [x] Retrieve data from database in API controller.
- [x] Drag&drop study tree builder (StudyCourse -> StudyGroup -> Study).
- [x] Save changes from frontend to database.
- [x] Data validation (min/max points ...).
- [x] Excel export.
- [x] Error logging.

## Quick start (frontend)

1) Make sure you have [Node.js](https://nodejs.org/en/) installed.
2) Navigate to the application main directory (`src/App`) from terminal where package.json is located.
3) Run `npm install` (**run as administrator**)
4) Run `npm run serve` to start the local development server and start prototyping.

#### Additional commands

+ Run `npm run build-dev` to build in development mode application
+ Run `npm run build-prod` - build in production mode application

## API
* http://localhost:9000/api/study
  * `[GET] '/'` - Get projects list.
  * `[POST] '/'` - Create new project.
  * `[GET] '/{projectID}'` - Get project tree data.
  * `[PUT] '/{projectID}'` - Update cell value.
  * `[POST] '/{projectID}'` - Save project changes.
  * `[DELETE] '/{projectID}'` - Delete project.
  * `[PATCH] '/{projectID}'` - Restore project.
* http://localhost:9000/api/export
  * `[GET] '/{projectID}'` - Get project excel document.

## Helpful links

+ [Vue Argon Design System](https://demos.creative-tim.com/vue-argon-design-system/documentation/)
+ [Vue.Draggable - Vue component (Vue.js 2.0) or directive (Vue.js 1.0) allowing drag-and-drop and synchronization with view model array.](https://github.com/SortableJS/Vue.Draggable)
+ [SlimGrid - A wrapper for SlickGrid to slim down the amount of time and code required to create a grid.](https://github.com/rob-white/SlimGrid)
+ [SlickGrid - Advanced JavaScript grid/spreadsheet component.](https://github.com/mleibman/SlickGrid)
+ [gitmoji - An emoji guide for your commit messages](https://gitmoji.carloscuesta.me/)
