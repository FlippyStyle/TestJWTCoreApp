import React, { Component } from 'react';
import './Users.css';

export class Users extends Component {
    static displayName = Users.name;

    constructor(props) {
        super(props);

        this.state = {
            isUserAuthenticated: sessionStorage.getItem("accessToken") !== null,
            roles: [],
            rolesLoading: true,
            usersLoading: true,
            user: {
                id: 0,
                login: "",
                name: "",
                email: "",
                password: "",
                role: 0,
                roleDisplayName: ""
            },
            users: []
        };
        if (!this.state.isUserAuthenticated) {
            this.props.history.push('/login');
        }

        this.submitForm = this.submitForm.bind(this);
        this.handleEdit = this.handleEdit.bind(this);
        this.handleRemove = this.handleRemove.bind(this);
        this.handleReset = this.handleReset.bind(this);
        this.handleInputChange = this.handleInputChange.bind(this);
        this.handleLoginChange = this.handleLoginChange.bind(this);
    }

    componentDidMount() {
        this.GetUsers();
        this.GetRoles();
    }

    async GetUsers() {
        var token = sessionStorage.getItem('accessToken');
        const response = await fetch("/api/users", {
            method: "GET",
            headers: {
                "Accept": "application/json",
                "Authorization": "Bearer " + token 
            }
        });

        if (response.ok === true) {
            const users = await response.json();      

            this.setState({
                users: users,
                usersLoading: false
            })
        }
    }

    async GetUser(id) {
        var token = sessionStorage.getItem('accessToken');
        const response = await fetch("/api/users/" + id, {
            method: "GET",
            headers: {
                "Accept": "application/json",
                "Authorization": "Bearer " + token 
            }
        });
        if (response.ok === true) {
            const user = await response.json();
            this.setState({
                user: {
                    id: id,
                    login: user.login,
                    name: user.name,
                    email: user.email,
                    password: user.password,
                    role: user.role,
                    roleDisplayName: user.roleDisplayName
                }
            });
        }
    }

    async CreateUser(user) {
        var token = sessionStorage.getItem('accessToken');

        const response = await fetch("api/users", {
            method: "POST",
            headers: {
                "Accept": "application/json", "Content-Type": "application/json",
                "Authorization": "Bearer " + token 
            },
            body: JSON.stringify({
                login: user.login,
                name: user.name,
                email: user.email,
                password: user.password,
                role: parseInt(user.role, 10),
                roleDisplayName: user.roleDisplayName
            })
        });
        if (response.ok === true) {
            const user = await response.json();
            this.handleReset();

            let users = this.state.users;
            users.push(user);
            this.setState({
                users: users
            })
        }
    }

    async EditUser(user) {
        var token = sessionStorage.getItem('accessToken');
        const response = await fetch("api/users", {
            method: "PUT",
            headers: {
                "Accept": "application/json", "Content-Type": "application/json",
                "Authorization": "Bearer " + token  
            },
            body: JSON.stringify({
                id: user.id,
                login: user.login,
                name: user.name,
                email: user.email,
                password: user.password,
                role: parseInt(user.role, 10),
                roleDisplayName: user.roleDisplayName
            })
        });
        if (response.ok === true) {
            const editedUser = await response.json();
            this.handleReset();

            let users = this.state.users;            
            let userIndex = users.findIndex(x => x.id === editedUser.id);

            if (userIndex > -1) {
                users[userIndex] = editedUser;
                this.setState({
                    users: users
                })
            }
        }
    }

    async DeleteUser(id) {
        var token = sessionStorage.getItem('accessToken');
        const response = await fetch("/api/users/" + id, {
            method: "DELETE",
            headers: {
                "Accept": "application/json",
                "Authorization": "Bearer " + token 
            }
        });
        if (response.ok === true) {
            const deletedUser = await response.json();

            let users = this.state.users;
            let userIndex = users.findIndex(x => x.id === deletedUser.id);

            if (userIndex > -1) {
                users.splice(userIndex, 1);
                this.setState({
                    users: users
                })
            }
        }
    }
    async GetRoles() {
        var token = sessionStorage.getItem('accessToken');
        const response = await fetch("api/roles", {
            method: "GET",
            headers: {
                "Accept": "application/json", "Content-Type": "application/json",
                "Authorization": "Bearer " + token 
            }
        });

        if (response.ok === true) {
            const roles = await response.json();
            this.setState({ roles: roles, rolesLoading: false });
        }
    }

    handleReset() {
        this.setState({
            user: {
                id: 0,
                login: "",
                name: "",
                password: "",
                email: "",
                role: 0,
                roleDisplayName: ""
            }
        })
    }

    handleInputChange(event) {
        const target = event.target;
        const value = target.value;
        const name = target.name;

        var user = this.state.user;
        user[name] = value;
        this.setState({
            user: user
        });
    }

    handleLoginChange(event) {
        var user = this.state.user;
        if (user.id > 0) {
            return false;
        }

        const target = event.target;
        const value = target.value;
        const name = target.name;

        user[name] = value;
        this.setState({
            user: user
        });
    }

    renderRolesSelect(roles, role) {
        return (
            <select className="form-control" name="role" value={role} onChange={this.handleInputChange}>
                {roles.map((option, index) =>
                    <option key={option.value} value={option.value}>
                        {option.text}
                    </option>
                )}
            </select>
        );
    }

    renderUsersTbody(users) {
        return (
            <tbody>
                {users.map((user, index) =>
                    <tr key={user.id}>
                        <td>{user.id}</td>
                        <td>{user.login}</td>
                        <td>{user.name}</td>
                        <td>{user.email}</td>
                        <td>{user.password}</td>
                        <td>{user.roleDisplayName}</td>
                        <td>
                            <a className="table-button" onClick={() => this.handleEdit(user.id)}>Изменить</a>
                            <a className="table-button" onClick={() => this.handleRemove(user.id)}>Удалить</a>
                        </td>
                    </tr>
                )}
            </tbody>
        );
    }

    submitForm(event) {
        event.preventDefault();
        if (this.state.user.id == 0)
            this.CreateUser(this.state.user);
        else
            this.EditUser(this.state.user);
    }

    handleEdit(id) {
        this.GetUser(id);
    }

    handleRemove(id) {
        this.DeleteUser(id);
    }

    render() {
        let usersRows = this.state.usersLoading
            ? <tbody></tbody>
            : this.renderUsersTbody(this.state.users);

        let select = this.state.rolesLoading
            ? <select></select>
            : this.renderRolesSelect(this.state.roles, this.state.user.role);
        return (
            <div>
                <h2>Создание/редактирование пользователя</h2>
                <form name="userForm" onSubmit={this.submitForm}>
                    <input type="hidden" name="id" value={this.state.user.id} />
                    <div className="form-group col-md-5">
                        <label htmlFor="name">Логин:</label>
                        <input className="form-control" name="login" value={this.state.user.login} onChange={this.handleLoginChange} />
                    </div>
                    <div className="form-group col-md-5">
                        <label htmlFor="name">Имя:</label>
                        <input className="form-control" name="name" value={this.state.user.name} onChange={this.handleInputChange} />
                    </div>
                    <div className="form-group col-md-5">
                        <label htmlFor="email">Email:</label>
                        <input className="form-control" name="email" type="email" value={this.state.user.email} onChange={this.handleInputChange} />
                    </div>
                    <div className="form-group col-md-5">
                        <label htmlFor="password">Пароль:</label>
                        <input className="form-control" name="password" type="password" value={this.state.user.password} onChange={this.handleInputChange} />
                    </div>
                    <div className="form-group col-md-5">
                        <label htmlFor="role">Роль:</label>
                        {select}
                    </div>
                    <div className="panel-body">
                        <button type="submit" id="submit" className="btn btn-primary">Сохранить</button>
                        <a id="reset" className="btn btn-primary" onClick={this.handleReset}>Сбросить</a>
                    </div>
                </form>
                <h2>Список пользователей</h2>
                <table className="table table-condensed table-striped col-md-6">
                    <thead>
                        <tr>
                            <th>Id</th>
                            <th>Логин</th>
                            <th>Имя</th>
                            <th>Email</th>
                            <th>Пароль</th>
                            <th>Роль</th>
                            <th></th>
                        </tr>
                    </thead>
                        {usersRows}
                </table>
            </div>
        );
    }
}
