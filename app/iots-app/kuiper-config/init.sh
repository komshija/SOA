/kuiper/bin/kuiperd &
sleep 1;
/kuiper/bin/kuiper create stream co '(value float, date string, dataName string) WITH ( DATASOURCE = "device/co/messages", FORMAT = "JSON")';
/kuiper/bin/kuiper create stream no2 '(value float, date string, dataName string) WITH ( DATASOURCE = "device/no2/messages", FORMAT = "JSON")';
/kuiper/bin/kuiper create rule corule '{"sql": "SELECT * from co where value > 1.0", "actions": [ { "log": {} }, { "mqtt": { "server": "mqtt:1883", "topic": "device/co/command" }}]}';
/kuiper/bin/kuiper create rule no2rule '{"sql": "SELECT * from no2 where value > 10.0", "actions": [ { "log": {} }, { "mqtt": { "server": "mqtt:1883", "topic": "device/no2/command"}}]}';
wait;

