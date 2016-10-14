"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
var core_1 = require('@angular/core');
var platform_browser_1 = require('@angular/platform-browser');
var router_1 = require('@angular/router');
var forms_1 = require('@angular/forms');
var app_component_1 = require('./app.component');
var listStudent_component_1 = require('./listStudent.component');
var pageNotFound_component_1 = require('./pageNotFound.component');
var editStudent_component_1 = require('./editStudent.component');
var AppModule = (function () {
    function AppModule() {
    }
    AppModule = __decorate([
        core_1.NgModule({
            imports: [
                platform_browser_1.BrowserModule,
                forms_1.FormsModule,
                router_1.RouterModule.forRoot([
                    {
                        path: 'student',
                        children: [
                            {
                                path: ':id',
                                component: editStudent_component_1.EditStudentComponent
                            }
                        ],
                        data: {
                            title: 'Student'
                        }
                    },
                    { path: '', component: listStudent_component_1.StudentListComponent },
                    { path: '**', component: pageNotFound_component_1.PageNotFoundComponent }
                ])],
            declarations: [
                app_component_1.AppComponent,
                listStudent_component_1.StudentListComponent,
                pageNotFound_component_1.PageNotFoundComponent,
                editStudent_component_1.EditStudentComponent],
            bootstrap: [app_component_1.AppComponent]
        }), 
        __metadata('design:paramtypes', [])
    ], AppModule);
    return AppModule;
}());
exports.AppModule = AppModule;
//# sourceMappingURL=app.module.js.map