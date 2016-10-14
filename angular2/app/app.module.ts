import { NgModule }      from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule }   from '@angular/router';
import { FormsModule }   from '@angular/forms';
import { AppComponent }   from './app.component';
import {StudentListComponent} from './listStudent.component'
import {PageNotFoundComponent} from './pageNotFound.component'
import {EditStudentComponent} from './editStudent.component'

@NgModule({
    imports: [
        BrowserModule,
        FormsModule,
        RouterModule.forRoot([
            {
                path: 'student',
                children: [
                    {
                        path: ':id',
                        component: EditStudentComponent
                    }
                ],
                data: {
                    title: 'Student'
                }
            },
            { path: '', component: StudentListComponent },
            { path: '**', component: PageNotFoundComponent }
        ])],
    declarations: [
        AppComponent,
        StudentListComponent,
        PageNotFoundComponent,
        EditStudentComponent],
    bootstrap: [AppComponent]
})
export class AppModule { }
