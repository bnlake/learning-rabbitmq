import { useState, useEffect } from "react";

import WorkerComponent from "./Worker";
import Worker from "../models/Worker";
import WorkersClient from "../services/WorkersClient";

function WorkerList() {
	const [workers, setWorkers] = useState<Array<Worker>>([]);

	useEffect(() => {
		async function fetchWorkers() {
			const result = await client.GetAll();
			setWorkers(() => result);
		}
		const client = new WorkersClient("https://localhost:7140");
		fetchWorkers();
	}, []);

	return (
		<div>
			<h1>Workers</h1>
			<ul>
				{workers.map((worker) => (
					<li>
						<WorkerComponent key={worker.id} worker={worker} />
					</li>
				))}
			</ul>
		</div>
	);
}

export default WorkerList;
