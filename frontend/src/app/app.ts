import { Component, OnInit, inject, signal } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatListModule } from '@angular/material/list';
import { MatToolbarModule } from '@angular/material/toolbar';
import { Todo } from './todo.model';
import { TodoService } from './todo.service';

@Component({
  selector: 'app-root',
  imports: [
    ReactiveFormsModule,
    MatToolbarModule,
    MatCardModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatCheckboxModule,
    MatListModule,
    MatIconModule,
  ],
  templateUrl: './app.html',
  styleUrl: './app.css',
})
export class App implements OnInit {
  private todoService = inject(TodoService);

  
  remainingTodos = signal<Todo[]>([]);
  completedTodos = signal<Todo[]>([]);

  todoForm = new FormGroup({
    title: new FormControl('', {
      nonNullable: true,
      validators: [Validators.required, Validators.maxLength(200)],
    }),
  });

  ngOnInit() {
    this.loadTodos();
  }

  loadTodos() {
    this.todoService.getAll().subscribe((todos) => {
      this.remainingTodos.set(todos.filter((todo) => !todo.isCompleted));
      this.completedTodos.set(todos.filter((todo) => todo.isCompleted));
    });
  }

  addTodo() {
    const title = this.todoForm.controls.title.value.trim();
    if (!title) {
      return;
    }
    this.todoForm.reset();
    this.todoService.add(title).subscribe(() => this.loadTodos());
  }

  toggleTodo(todo: Todo) {
    const changedTodo = { ...todo, isCompleted: !todo.isCompleted };
    this.todoService.update(changedTodo).subscribe(() => this.loadTodos());
  }

  deleteTodo(id: string) {
    this.todoService.delete(id).subscribe(() => this.loadTodos());
  }
}
