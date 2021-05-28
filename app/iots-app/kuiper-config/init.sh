sleep 15;
/kuiper/bin/kuiperd &
sleep 5;
/kuiper/bin/kuiper create stream co '(value float, date string, dataName string) WITH ( DATASOURCE = "device/co/messages", FORMAT = "JSON")';
/kuiper/bin/kuiper create stream no2 '(value float, date string, dataName string) WITH ( DATASOURCE = "device/no2/messages", FORMAT = "JSON")';
/kuiper/bin/kuiper create rule corule '{"sql": "SELECT * from co where value > 5", "actions": [ { "log": {} }, { "mqtt": { "server": "mqtt:1883", "topic": "device/co/command" }}]}';
/kuiper/bin/kuiper create rule no2rule '{"sql": "SELECT * from no2 where value > 150", "actions": [ { "log": {} }, { "mqtt": { "server": "mqtt:1883", "topic": "device/no2/command"}}]}';
wait;

