import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { Todo } from './todo.model';

// Talks to the .NET API. One method per backend call.
@Injectable({ providedIn: 'root' })
export class TodoService {
  private http = inject(HttpClient);
  private url = '/api/todos';

  getAll() {
    return this.http.get<Todo[]>(this.url);
  }

  add(title: string) {
    return this.http.post<Todo>(this.url, { title });
  }

  update(todo: Todo) {
    return this.http.put<Todo>(`${this.url}/${todo.id}`, {
      title: todo.title,
      isCompleted: todo.isCompleted,
    });
  }

  delete(id: string) {
    return this.http.delete(`${this.url}/${id}`);
  }
}
