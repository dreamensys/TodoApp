import React, { Component } from 'react'
import { connect } from 'react-redux'
import { loadTodos, addTodo, toggleTodo, deleteTodo } from '../actions/actionCreators'
import InputBox from '../components/InputBox'
import TodoList from '../components/TodoList'
import getTodoList from '../services/getTodoListService'
import createTodo from '../services/createTodoService'
import updateTodo from '../services/updateTodoService'
import removeTodo from '../services/deleteTodoService'
class Todos extends Component {

	async getTodos() {
		const todos = await getTodoList();
		this.props.dispatch(loadTodos(todos));
	}

	createTodo = async (title) => {
		if (!(title === '')) {
			const result = await createTodo({title: title});
			this.props.dispatch(addTodo(result.id, result.title))
		}    
	}

	updateTodo = async (params) => {
		await updateTodo({id: params.id, completed: params.checked, title: params.title});
		this.props.dispatch(toggleTodo(params.id))
	}

	deleteTodo = async (id) => {
		await removeTodo(id);
		this.props.dispatch(deleteTodo(id))
	}

  	componentDidMount() {
    	this.getTodos();
  	}

	render() {
		return (
			<div className="container">
				<InputBox createTodo={this.createTodo} />
				<TodoList todos={this.props.todos} 
					updateTodo={this.updateTodo} 
					deleteTodo={this.deleteTodo} />
			</div>
		)
	}
}

const mapStateToProps = (state) => {
	return {
		todos: state.todos
	}
}

export default connect(mapStateToProps)(Todos)
