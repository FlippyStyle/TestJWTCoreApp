import React, { Component } from 'react';
import { Route } from 'react-router';
import { Redirect } from 'react-router-dom';
import { Layout } from './components/Layout';
import { Login } from './components/login/Login';
import { Users } from './components/users/Users';

import './custom.css'

export default class App extends Component {

    constructor() {
        super();
        this.state = {
            isUserAuthenticated: sessionStorage.getItem("accessToken") !== null
        };
    }

    render() {
        return (
            <Layout>
                <Route
                    exact
                    path="/"
                    render={() => {
                        return (
                            this.state.isUserAuthenticated ?
                                <Redirect to="/users" /> :
                                <Redirect to="/login" />
                        )
                    }}
                />
                <Route path='/login' component={Login} />
                <Route path='/users' component={Users} />
            </Layout>
        );
    }
}
