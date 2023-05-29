import { useState, useEffect } from "react";

import WorkerComponent from "./Worker";
import Worker from "../models/Worker";
import WorkersClient from "../services/WorkersClient";

function WorkerList() {
	const [workers, setWorkers] = useState<Array<Worker>>([]);

	useEffect(() => {
		const controller = new AbortController();

		async function fetchWorkers() {
			const result = await client.GetAll(controller.signal);
			setWorkers(() => result);
		}
		const api_url = import.meta.env.API_URL ?? import.meta.env.VITE_API_URL;
		const client = new WorkersClient(api_url);
		fetchWorkers();

		return () => controller.abort();
	}, []);

	return (
		<div>
			<h1>Workers</h1>
			<ul>
				{workers.map((worker) => (
					<li key={worker.id}>
						<WorkerComponent worker={worker} />
					</li>
				))}
			</ul>
		</div>
	);
}

export default WorkerList;
