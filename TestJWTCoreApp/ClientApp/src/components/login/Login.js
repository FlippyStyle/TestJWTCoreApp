import React, { Component } from 'react';

export class Login extends Component {    
    constructor(props) {
        super(props);
        this.handleSubmit = this.handleSubmit.bind(this);

        this.state = {
            login: "",
            password: ""
        }

        this.handleInputChange = this.handleInputChange.bind(this);
    }

    handleInputChange(event) {
        const target = event.target;
        const value = target.value;
        const name = target.name;

        var state = this.state;
        state[name] = value;
        this.setState(state);
    }

    async handleSubmit(event) {
        event.preventDefault();
        const response = await fetch("/token", {
            method: "POST",
            headers: { "Accept": "application/json", "Content-Type": "application/json" },
            body: JSON.stringify({
                login: this.state.login,
                password: this.state.password,
            })
        });
        const data = await response.json();

        if (response.ok === true) {
            sessionStorage.setItem("accessToken", data.accessToken);
            this.props.history.push('/users');
        }
        else {
            // handle error
             console.log("Error: ", response.status, data.errorText);
        }
    }

    render() {
        return (
            <div>
                <h1>Вход в систему</h1>
                <form name="loginForm" onSubmit={this.handleSubmit}>
                    <div className="form-group col-md-5">
                        <label htmlFor="name">Логин:</label>
                        <input className="form-control" name="login" value={this.state.login} onChange={this.handleInputChange} />
                    </div>
                    <div className="form-group col-md-5">
                        <label htmlFor="age">Пароль:</label>
                        <input className="form-control" name="password" type="password" value={this.state.password} onChange={this.handleInputChange} />
                    </div>
                    <div className="panel-body">
                        <button type="submit" id="submit" className="btn btn-primary">Войти</button>
                    </div>
                </form>
            </div>
        );
    }
}
