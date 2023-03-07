<template>
    <div class="post">
        <div v-if="loading" class="loading">
            Loading... Please refresh once the ASP.NET backend has started.
        </div>
        <div>
            <input v-model="input" placeholder="Type task name here">
            <button @click="postData">Add</button>
        </div>
        <div v-if="post" class="content">
            <ul>
                <li v-for="task in post" :key="task.id">
                    <input type="checkbox" v-model="task.is_done" @click="updateTask(task)">
                    <button @click="deleteTask(task.id)">X</button>
                    <span :class="{ done: task.is_done }"> {{ task.name }} </span>                    
                </li>
            </ul>    
    </div>
    </div>
</template>

<script lang="js">
    import Vue from 'vue';

    export default Vue.extend({
        data() {
            return {
                loading: false,
                post: null,                
                input: '' 
            };
        },
        created() {            
            this.fetchData();           
        },
        watch: {            
            '$route': 'fetchData'
        },
        methods: { 
            fetchData() {
                this.post = null;                
                this.loading = true;                
                this.input = '';               

                fetch('job')
                    .then(r => r.json())
                    .then(json => {
                        this.post = json;
                        this.loading = false;
                        return;
                    });
            },            
            async postData() {
                let request = {
                    "name": this.input
                };
                const response = await fetch('job', {
                    method: 'POST', 
                    mode: 'cors', 
                    cache: 'no-cache', 
                    credentials: 'same-origin', 
                    headers: {
                        'Content-Type': 'application/json'
                    },
                    redirect: 'follow', 
                    referrerPolicy: 'no-referrer', 
                    body: JSON.stringify(request) 
                });
                this.fetchData();
                return await response.json();
            },            
            async deleteTask(id) {
                const response = await fetch('job' + '/' + id, {
                    method: 'DELETE',
                    mode: 'cors',
                    cache: 'no-cache', 
                    credentials: 'same-origin', 
                    headers: {
                        'Content-Type': 'application/json'
                    },
                    redirect: 'follow',
                    referrerPolicy: 'no-referrer', 
                    
                });
                this.fetchData();
                return await response.json();
            },
            async updateTask(task) {
                task.is_done = task.is_done == true ? false : true ;

                const response = await fetch('job' + '/' + task.id, {
                    method: 'PUT',
                    mode: 'cors',
                    cache: 'no-cache',
                    credentials: 'same-origin',
                    headers: {
                        'Content-Type': 'application/json'
                    },
                    redirect: 'follow',
                    referrerPolicy: 'no-referrer',
                    body: JSON.stringify(task)
                });
                this.fetchData();
                return await response.json();
            }
        },
    });
</script>

<style>
    .done {
        text-decoration: line-through;
    }
    .content {
        text-align: left;
    }
</style>