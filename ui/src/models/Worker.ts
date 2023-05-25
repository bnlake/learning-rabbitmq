export default class Worker {
	constructor(public name: string, public id: string = crypto.randomUUID()) {}
}
