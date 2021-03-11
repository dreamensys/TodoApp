import React, { Component } from 'react'

class TodoItem extends Component {
	constructor(props) {
		super(props);
    }

    updateTodo = (e, id, title) => {
        this.props.updateTodo({id: id, checked: e.target.checked, title: title})
    }

    deleteTodo = (id) => {
        this.props.deleteTodo(id)
    }    

    render() {
        const todo = this.props.todo;
        return(
            <li className="task" key={todo.id} id={todo.id}>
                <input className="todo-check" type="checkbox" checked={todo.completed} onChange={(e) => this.updateTodo(e, todo.id, todo.title)} />              
                <label className="todo-label">{todo.title}</label>
                <span className="delete-todo-item-btn" onClick={(e) => this.deleteTodo(todo.id)}>
                x
                </span>
            </li>
        );
    }
}

export default TodoItem;